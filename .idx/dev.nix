{ pkgs, ... }:
{
  channel = "stable-24.05";

  packages = [
    pkgs.dotnet-sdk_8
    pkgs.netcoredbg
  ];

  env = {
    DOTNET_CLI_TELEMETRY_OPTOUT = "1";
    DOTNET_NOLOGO = "1";
  };

  idx = {
    extensions = [
      "ms-dotnettools.csdevkit"
      "ms-dotnettools.csharp"
    ];

    workspace = {
      onCreate = {
        dotnet-restore = "dotnet restore RhbkSdk.sln";
      };
      onStart = {
        dotnet-build = "dotnet build RhbkSdk.sln";
      };
    };
  };
}
