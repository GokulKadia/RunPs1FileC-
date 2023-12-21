
Get-appxprovisionedpackage –online | where-object {$_.packagename –like '*IntelGraphicsExperience*'} | Remove-AppxProvisionedPackage -online

sleep 15

Get-AppxPackage -AllUsers -PackageTypeFilter All -Name "*IntelGraphicsExperience*" | Remove-AppxPackage -AllUsers

$Driver = Get-WindowsDriver –Online -All | ? {($_.OriginalFileName -like "*igcc_dch.inf*")}
$OemXX = $Driver.Driver
pnputil /delete-driver $OemXX /uninstall /force

