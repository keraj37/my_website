param([String]$value="false")

$myResourceGroup = '01'
$mySite = 'jerryswitalskiwebapp'

$webApp = Get-AzureRMWebAppSlot -ResourceGroupName $myResourceGroup -Name $mySite -Slot production
$appSettingList = $webApp.SiteConfig.AppSettings

$hash = @{}
ForEach ($kvp in $appSettingList) {
    $hash[$kvp.Name] = $kvp.Value
}

$hash['sendDataCollectionEmails'] = $value

Set-AzureRMWebAppSlot -ResourceGroupName $myResourceGroup -Name $mySite -AppSettings $hash -Slot production