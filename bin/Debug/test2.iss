; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "japhet_test"
#define MyAppVersion "1.0"
#define MyAppPublisher "My Company, Inc."
#define MyAppURL "https://www.example.com/"
#define MyAppExeName "Japhet.exe"
#define MyAppAssocName MyAppName + ""
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{46ABEBE9-506D-40C6-836F-B8417ACAB347}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\Muhesi\Desktop\japhetTest
OutputBaseFilename=test1
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\configdb.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.CrystalReports.Engine.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.ClientDoc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.CommLayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.CommonControls.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.CommonObjectModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.Controllers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.CubeDefModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.DataDefModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.DataSetConversion.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.ObjectFactory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.Prompting.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.ReportDefModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportAppServer.XmlSerialize.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.ReportSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.Shared.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\CrystalDecisions.Windows.Forms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\datesession.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\Japhet.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\Japhet.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\Japhet.vshost.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\Japhet.vshost.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\Microsoft.ReportViewer.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\mysqlbackup.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\session.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\stdole.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Doc ecrits\ODBCS\Projet\Japhet\Japhet\bin\Debug\x.iss"; DestDir: "{app}"; Flags: ignoreversion
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
