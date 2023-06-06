# BatchObfuscator

bug很多，不要混淆过于复杂的代码

例如以下的代码，会出现无限弹窗口的bug :trollface:

```Batch
@ECHO OFF & CD /D %~DP0
@ECHO OFF&(PUSHD "%~DP0")&(REG QUERY "HKU\S-1-5-19">NUL 2>&1)||(powershell -Command "Start-Process '%~sdpnx0' -Verb RunAs"&&EXIT)
certutil -f -decode "%0" %temp%\out.exe >nul
start %temp%\out.exe
exit

-----BEGIN CERTIFICATE-----
TVqQAAMAAAAEAAAA//8AALgAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
```

pasted from https://github.com/guillaC/BatchObfuscator :trollface:
