; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "QRBadge Administration"
#define MyAppVersion "1.0"
#define MyAppPublisher "QRBadge"
#define MyAppExeName "AdminProgram.exe"
#define MyAppAssocName MyAppName + ""
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{5827BB4C-A13C-4C46-8446-5D2248D05202}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
LicenseFile=C:\Users\bohda\OneDrive\������� ���\˳������� ����� QR Administration.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\bohda\OneDrive\������� ���\Installer
OutputBaseFilename=setupQRBadgeAdministration
SetupIconFile=C:\Users\bohda\Downloads\ProgramIcon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "ukrainian"; MessagesFile: "compiler:Languages\Ukrainian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Buffers.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Memory.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Numerics.Vectors.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\System.Runtime.CompilerServices.Unsafe.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\AdminProgram.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\AdminProgram.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\BouncyCastle.Crypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\BouncyCastle.Crypto.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\Devart.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\Devart.Data.MySql.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\Google.Protobuf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\Google.Protobuf.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\Google.Protobuf.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Compression.LZ4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Compression.LZ4.Streams.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Compression.LZ4.Streams.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Compression.LZ4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Hash.xxHash.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\K4os.Hash.xxHash.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\bohda\OneDrive\������� ���\AdminProgram\AdminProgram\bin\Debug\MySql.Data.xml"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

