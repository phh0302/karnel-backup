# =====================================================================
# Karnel Travel Guide - point www.karneltravelguide.com to this PC (CRS FR-01)
# Run in PowerShell **as Administrator**:
#   powershell -ExecutionPolicy Bypass -File scripts\setup-hosts.ps1
# Add -Remove to take the entries out again.
# =====================================================================
param([switch]$Remove)

# Editing the hosts file needs Administrator rights: stop early with a clear message
$isAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Host "Please run PowerShell as Administrator (right-click PowerShell > Run as administrator), then run this script again." -ForegroundColor Yellow
    exit 1
}

$hosts   = "$env:SystemRoot\System32\drivers\etc\hosts"
$entries = @("127.0.0.1 www.karneltravelguide.com", "127.0.0.1 karneltravelguide.com")

# Read the current file and drop any old Karnel lines so the script can run twice safely
$lines = Get-Content $hosts | Where-Object { $_ -notmatch "karneltravelguide\.com" }

if (-not $Remove) {
    $lines += "# Karnel Travel Guide (eProject)"
    $lines += $entries
}

# Some PCs mark the hosts file read-only; clear that flag before writing
(Get-Item $hosts).IsReadOnly = $false
# Write with .NET instead of Set-Content: Set-Content fails with "Stream was not readable"
# when another program (antivirus, DNS service) has the hosts file open. Retry a few times.
$written = $false
for ($i = 1; $i -le 5 -and -not $written; $i++) {
    try {
        [System.IO.File]::WriteAllLines($hosts, [string[]]$lines, [System.Text.Encoding]::ASCII)
        $written = $true
    } catch {
        Start-Sleep -Milliseconds 500
    }
}
if (-not $written) {
    Write-Host "Could not write $hosts. An antivirus may be protecting it: turn off its hosts-file protection, or add the lines by hand (see Docs\Reports\Setup_Guide.md)." -ForegroundColor Red
    exit 1
}

if (-not $Remove) { Write-Host "Added: $($entries -join ', ')" } else { Write-Host "Removed the karneltravelguide.com entries." }
ipconfig /flushdns | Out-Null
Write-Host "Done. Start the site with the 'karneltravelguide.com' profile, then open http://www.karneltravelguide.com"
