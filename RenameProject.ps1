
## Input Usuario
$NomeProjeto = Read-Host 'Nome do Projeto';

## Variaveis
$NomeBase = 'RotaViagem';
$Exclude = @("*.ps1", "*.md")


Write-Host "- Renomeando projeto de ($NomeBase) para ($NomeProjeto)";

$count = 1;
Write-Host "1 - Renomeando nome de arquivos...";
Get-ChildItem -Include "*$NomeBase*" -Exclude $Exclude -File -Recurse | ForEach `
{
    $NomeReplace = $_.Name -replace ($NomeBase,$NomeProjeto); `
    Write-Host " 1.$count - Arquivo: ($_) => ($NomeReplace) "; `
    Rename-Item -Path $_ -NewName ($_.Name.Replace($NomeBase,$NomeProjeto));
    $count++; `
};

$count = 1;
Write-Host "2 - Renomeando conteudo de arquivos...";
Get-ChildItem -Exclude $Exclude -File -Recurse | Where-Object { Select-String -Pattern $NomeBase $_ -Quiet } | ForEach `
{
    Write-Host " 2.$count - Arquivo: ($_)"; `
    $conteudoReplace = (Get-Content -Path $_ -Raw) -replace ($NomeBase,$NomeProjeto); `
    Set-Content -Path $_.FullName -Value $conteudoReplace; `
    $count++;
};

$count = 1;
Write-Host "3 - Renomeando nome das pastas..."
Get-ChildItem -Path "*" -Filter "*$NomeBase*" -Directory -Recurse | ForEach-Object `
{ 
    $FullFolderName = $_.FullName; `
    $NewFolderName = $_.Name -replace ($NomeBase,$NomeProjeto); `
    Write-Host " 3.$count - Pasta: ($FullFolderName) => $NewFolderName"; `
    Rename-Item -Path $_.FullName -NewName $NewFolderName; `
    $count++;
};

Read-Host “Pressione ENTER para concluir...”