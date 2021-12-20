cmd Run As Admin

ไปยัง Folder ที่จะติดตั้งแล้ว install ด้วย command ด้านล่าง

Rabby2_ModbusProtocol_WindowsService.exe --install
Rabby2_ModbusProtocol_WindowsService.exe --uninstall

*case ลืม stop ก่อน uninstall 
- cmd C:\Windows\system32 > sc delete <service name> 
service name check จาก service คลิกขวา service ที่จะลบ > property > Service name