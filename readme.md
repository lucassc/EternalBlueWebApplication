# Eternal Blue Web Application

## Credentials

This application credentials are stored at Azure Vault. 

To run this application you need configure the variables on `appsettings.json`.

```json
  "AzureKeyVault": {
      "DNS": "Your DNS here", //eg: https://eternalblue.vault.azure.net/
      "ClientId": "Client id here",
      "ClientSecret": "Client Secret here"
}
```

[How to Create the Application Client ID and Client Secret from Microsoft Azure New Portal?  - URL](https://www.bizmerlin.com/articles/how-to-create-the-application-client-id-and-client-secret-from-microsoft-azure-new-portal/)

The secrets names are defined on `appsettings.json` to:

```json
  "AzureSecretsName": {
        "FirstPassword": "FirstPassword",
        "FirstPasswordAscii": "FirstPasswordASCII",
        "SecondPassword": "SecondPassword"
}
```