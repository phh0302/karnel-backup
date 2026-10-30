# =====================================================================
# Karnel Travel Guide - point www.karneltravelguide.com to this PC (CRS FR-01)
# Run in PowerShell **as Administrator**:
#   powershell -ExecutionPolicy Bypass -File scripts\setup-hosts.ps1
# Add -Remove to take the entries out again.
# =====================================================================
param([switch]$Remove)

$hosts   = "$env:SystemRoot\System32\drivers\etc\hosts"
$entries = @("127.0.0.1 www.karneltravelguide.com", "127.0.0.1 karneltravelguide.com")

# Read the current file and drop any old Karnel lines so the script can run twice safely
$lines = Get-Content $hosts | Where-Object { $_ -notmatch "karneltravelguide\.com" }

if (-not $Remove) {
    $lines += "# Karnel Travel Guide (eProject)"
    $lines += $entries
    Write-Host "Added: $($entries -join ', ')"
} else {
    Write-Host "Removed the karneltravelguide.com entries."
}

Set-Content -Path $hosts -Value $lines -Encoding ASCII
ipconfig /flushdns | Out-Null
Write-Host "Done. Start the site with the 'karneltravelguide.com' profile, then open http://www.karneltravelguide.com"
