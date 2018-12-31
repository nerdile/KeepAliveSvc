# KeepAliveSvc
Keeps hard drives alive by writing to them on a regular basis.

## Configuration:
 * In Nerdile.KeepAliveSvc.exe.config
 * LogFolder: Path to folder where the service has access to write logs
 * Paths: Semicolon-separated list of folders to write pings to
 * Interval: Milliseconds between writes
 * Make sure that the service has the necessary access to the Paths and the LogFolders.
   After you've installed the service, you can grant Write permissions to "NT SERVICE\KeepAliveSvc".
   (The service runs as Local Service.)
 
## Installing:
    installutil Nerdile.KeepAliveSvc.exe
    net start KeepAliveSvc

## Uninstalling:
    net stop KeepAliveSvc
    installutil /u Nerdile.KeepAliveSvc.exe

