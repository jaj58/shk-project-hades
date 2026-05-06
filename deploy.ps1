# deploy.ps1 — Push server files / release ZIPs to projecthades.co.uk via HTTP POST.
#
# Modes
# -----
#   File deploy (default)
#     .\deploy.ps1                         # deploy api/ + admin/
#     .\deploy.ps1 -Only api               # deploy only api/
#     .\deploy.ps1 -Only admin             # deploy only admin/
#
#   Release deploy
#     .\deploy.ps1 -Release
#     .\deploy.ps1 -Release -Changelog "Fixed X, improved Y"
#
#     Auto-increments the patch version in AssemblyInfo.cs (e.g. 1.0.1.0 -> 1.0.2.0),
#     rebuilds the project, zips the exe, uploads to downloads/ and updates version.json.

param(
    # File-deploy params
    [string]$Only      = "all",   # "all", "api", or "admin"

    # Release params
    [switch]$Release,
    [string]$Changelog = ""       # optional release notes
)

# ============================================================
# SETTINGS — edit these when required
# ============================================================
$BaseUrl         = "https://projecthades.co.uk/api/deploy_api.php"
$Token           = "NR7B4ykUn6219DKaXrAMfvCxFoeiEmpV0OIjlcqQHP8gdG3T"   # must match api/deploy_config.php
$ZipPassword     = "34saskahjdako3249asd"       # password baked into release ZIPs

# Paths (relative to this script)
$AssemblyInfoPath = "StrongholdKingdoms\AssemblyInfo.cs"
$SolutionPath     = "StrongholdKingdoms\StrongholdKingdoms.sln"
$ExePath          = "StrongholdKingdoms\bin\Release\StrongholdKingdoms.exe"

# 7-Zip
$SevenZip         = "C:\Program Files\7-Zip\7z.exe"

# 4GB patch tool (relative to this script)
$PatchTool        = "4gb_patch.exe"
# ============================================================

$Root = $PSScriptRoot

# ---------- Helper: upload a single file ------------------------------------

function Deploy-File {
    param(
        [string]$LocalPath,
        [string]$RelPath
    )

    Write-Host "  Uploading $RelPath ... " -NoNewline

    $boundary = [System.Guid]::NewGuid().ToString("N")
    $fileBytes = [System.IO.File]::ReadAllBytes($LocalPath)
    $fileName  = [System.IO.Path]::GetFileName($LocalPath)

    $bodyParts = [System.Collections.Generic.List[byte]]::new()

    $pathHeader = "--$boundary`r`nContent-Disposition: form-data; name=`"path`"`r`n`r`n"
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($pathHeader))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($RelPath))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("`r`n"))

    $fileHeader = "--$boundary`r`nContent-Disposition: form-data; name=`"file`"; filename=`"$fileName`"`r`nContent-Type: application/octet-stream`r`n`r`n"
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($fileHeader))
    $bodyParts.AddRange($fileBytes)
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("`r`n"))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("--$boundary--`r`n"))

    try {
        $response = Invoke-WebRequest `
            -Uri         $BaseUrl `
            -Method      POST `
            -Headers     @{ 'X-Deploy-Token' = $Token } `
            -ContentType "multipart/form-data; boundary=$boundary" `
            -Body        $bodyParts.ToArray() `
            -UseBasicParsing `
            -ErrorAction Stop

        $json = $response.Content | ConvertFrom-Json
        if ($json.ok) {
            Write-Host "OK" -ForegroundColor Green
        } else {
            Write-Host "FAILED: $($json.error)" -ForegroundColor Red
            exit 1
        }
    } catch {
        Write-Host "FAILED: $_" -ForegroundColor Red
        exit 1
    }
}

# ---------- Helper: upload raw bytes (no file on disk) ----------------------

function Deploy-Bytes {
    param(
        [string]$RelPath,
        [string]$FileName,
        [byte[]]$Bytes
    )

    Write-Host "  Uploading $RelPath ... " -NoNewline

    $boundary = [System.Guid]::NewGuid().ToString("N")
    $bodyParts = [System.Collections.Generic.List[byte]]::new()

    $pathHeader = "--$boundary`r`nContent-Disposition: form-data; name=`"path`"`r`n`r`n"
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($pathHeader))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($RelPath))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("`r`n"))

    $fileHeader = "--$boundary`r`nContent-Disposition: form-data; name=`"file`"; filename=`"$FileName`"`r`nContent-Type: application/octet-stream`r`n`r`n"
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes($fileHeader))
    $bodyParts.AddRange($Bytes)
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("`r`n"))
    $bodyParts.AddRange([System.Text.Encoding]::UTF8.GetBytes("--$boundary--`r`n"))

    try {
        $response = Invoke-WebRequest `
            -Uri         $BaseUrl `
            -Method      POST `
            -Headers     @{ 'X-Deploy-Token' = $Token } `
            -ContentType "multipart/form-data; boundary=$boundary" `
            -Body        $bodyParts.ToArray() `
            -UseBasicParsing `
            -ErrorAction Stop

        $json = $response.Content | ConvertFrom-Json
        if ($json.ok) {
            Write-Host "OK" -ForegroundColor Green
        } else {
            Write-Host "FAILED: $($json.error)" -ForegroundColor Red
            exit 1
        }
    } catch {
        Write-Host "FAILED: $_" -ForegroundColor Red
        exit 1
    }
}

# ---------- Helper: deploy all files in a folder ----------------------------

function Deploy-Folder {
    param(
        [string]$FolderPath,
        [string]$RelPrefix
    )

    Write-Host "Deploying $RelPrefix/ ..." -ForegroundColor Cyan

    $files = Get-ChildItem -Path $FolderPath -File -Recurse
    foreach ($file in $files) {
        $rel = $file.FullName.Substring($Root.Length + 1).Replace('\', '/')
        Deploy-File -LocalPath $file.FullName -RelPath $rel
    }
}

# ---------- Release mode ----------------------------------------------------

if ($Release) {
    $asmPath = Join-Path $Root $AssemblyInfoPath
    $slnPath = Join-Path $Root $SolutionPath
    $exeFullPath = Join-Path $Root $ExePath

    # --- Validate prerequisites
    if (-not (Test-Path $asmPath)) {
        Write-Host "ERROR: AssemblyInfo.cs not found at: $asmPath" -ForegroundColor Red; exit 1
    }
    if (-not (Test-Path $slnPath)) {
        Write-Host "ERROR: Solution not found at: $slnPath" -ForegroundColor Red; exit 1
    }
    if (-not (Test-Path $SevenZip)) {
        Write-Host "ERROR: 7-Zip not found at: $SevenZip" -ForegroundColor Red
        Write-Host "  Install 7-Zip or update `$SevenZip in deploy.ps1." -ForegroundColor Yellow; exit 1
    }

    # --- Find MSBuild via vswhere
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (-not (Test-Path $vswhere)) {
        Write-Host "ERROR: vswhere.exe not found. Is Visual Studio installed?" -ForegroundColor Red; exit 1
    }
    $msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
    if (-not $msbuild) {
        Write-Host "ERROR: MSBuild not found via vswhere." -ForegroundColor Red; exit 1
    }

    # --- Read and increment version from AssemblyInfo.cs
    $asmContent = Get-Content $asmPath -Raw

    if ($asmContent -notmatch '\[assembly: AssemblyVersion\("(\d+)\.(\d+)\.(\d+)\.(\d+)"\)\]') {
        Write-Host "ERROR: Could not parse AssemblyVersion from AssemblyInfo.cs" -ForegroundColor Red; exit 1
    }
    $major = [int]$Matches[1]
    $minor = [int]$Matches[2]
    $patch = [int]$Matches[3] + 1   # auto-increment patch
    $build = [int]$Matches[4]

    $newVersion     = "$major.$minor.$patch.$build"    # e.g. 1.0.2.0  (for AssemblyInfo)
    $releaseVersion = "$major.$minor.$patch"            # e.g. 1.0.2    (for ZIP / version.json)

    Write-Host "Releasing version $releaseVersion ..." -ForegroundColor Cyan
    Write-Host ""

    # --- Update AssemblyInfo.cs
    Write-Host "  Updating AssemblyInfo.cs ($($major).$($minor).$($patch-1).$build -> $newVersion) ... " -NoNewline
    $asmContent = $asmContent -replace `
        '\[assembly: AssemblyVersion\("\d+\.\d+\.\d+\.\d+"\)\]', `
        "[assembly: AssemblyVersion(`"$newVersion`")]"
    $asmContent = $asmContent -replace `
        '\[assembly: AssemblyFileVersion\("\d+\.\d+\.\d+\.\d+"\)\]', `
        "[assembly: AssemblyFileVersion(`"$newVersion`")]"
    Set-Content $asmPath $asmContent -NoNewline
    Write-Host "OK" -ForegroundColor Green

    # --- Build Release
    Write-Host "  Building Release ... " -NoNewline
    $buildOutput = & $msbuild $slnPath /p:Configuration=Release /p:Platform="Any CPU" /v:minimal /nologo 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "FAILED" -ForegroundColor Red
        Write-Host $buildOutput
        # Revert AssemblyInfo.cs on build failure
        $asmContent = $asmContent -replace `
            '\[assembly: AssemblyVersion\("\d+\.\d+\.\d+\.\d+"\)\]', `
            "[assembly: AssemblyVersion(`"$major.$minor.$($patch-1).$build`")]"
        $asmContent = $asmContent -replace `
            '\[assembly: AssemblyFileVersion\("\d+\.\d+\.\d+\.\d+"\)\]', `
            "[assembly: AssemblyFileVersion(`"$major.$minor.$($patch-1).$build`")]"
        Set-Content $asmPath $asmContent -NoNewline
        Write-Host "  AssemblyInfo.cs reverted." -ForegroundColor Yellow
        exit 1
    }
    Write-Host "OK" -ForegroundColor Green

    # --- Apply 4GB patch to the exe
    $patchToolPath = Join-Path $Root $PatchTool
    if (-not (Test-Path $patchToolPath)) {
        Write-Host "ERROR: 4GB patch tool not found at: $patchToolPath" -ForegroundColor Red; exit 1
    }
    Write-Host "  Applying 4GB patch ... " -NoNewline
    $patchResult = & $patchToolPath $exeFullPath 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "FAILED" -ForegroundColor Red
        Write-Host $patchResult
        exit 1
    }
    Write-Host "OK" -ForegroundColor Green

    # --- Create password-protected ZIP
    $zipFileName = "hades_$releaseVersion.zip"
    $zipPath     = Join-Path $Root "builds\$zipFileName"
    $downloadUrl = "https://projecthades.co.uk/downloads/$zipFileName"

    New-Item -ItemType Directory -Path (Join-Path $Root "builds") -Force | Out-Null
    if (Test-Path $zipPath) { Remove-Item $zipPath -Force }

    Write-Host "  Creating ZIP ($zipFileName) ... " -NoNewline
    # cd to the exe's directory so 7-Zip only stores the filename, not the full path
    Push-Location (Split-Path $exeFullPath)
    $7zResult = & $SevenZip a -tzip -mem=AES256 "-p$ZipPassword" $zipPath (Split-Path $exeFullPath -Leaf) 2>&1
    Pop-Location
    if ($LASTEXITCODE -ne 0) {
        Write-Host "FAILED" -ForegroundColor Red
        Write-Host $7zResult
        exit 1
    }
    Write-Host "OK" -ForegroundColor Green
    Write-Host ""

    # --- Upload server files
    Deploy-Folder "$Root\api"   "api"
    Deploy-Folder "$Root\admin" "admin"
    Write-Host ""

    # --- Upload ZIP + version.json
    Deploy-File -LocalPath $zipPath -RelPath "downloads/$zipFileName"

    $versionJson = [ordered]@{
        version      = $releaseVersion
        download_url = $downloadUrl
        changelog    = $Changelog
        zip_password = $ZipPassword
    } | ConvertTo-Json -Compress

    Deploy-Bytes -RelPath "api/version.json" -FileName "version.json" -Bytes ([System.Text.Encoding]::UTF8.GetBytes($versionJson))

    Write-Host ""
    Write-Host "Release $releaseVersion deployed!" -ForegroundColor Green
    exit 0
}

# ---------- File deploy mode ------------------------------------------------

switch ($Only.ToLower()) {
    "api"   { Deploy-Folder "$Root\api"   "api"   }
    "admin" { Deploy-Folder "$Root\admin" "admin" }
    default {
        Deploy-Folder "$Root\api"   "api"
        Deploy-Folder "$Root\admin" "admin"
    }
}

Write-Host ""
Write-Host "Deploy complete!" -ForegroundColor Green
