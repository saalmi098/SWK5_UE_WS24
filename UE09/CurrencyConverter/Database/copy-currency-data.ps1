$targetDir = "$env:APPDATA/CurrencyConverter"
$dataFile  = "./currency-data.csv"

New-Item -ItemType Directory -Path $targetDir -ErrorAction SilentlyContinue
Copy-Item -Path $dataFile -Destination $targetDir