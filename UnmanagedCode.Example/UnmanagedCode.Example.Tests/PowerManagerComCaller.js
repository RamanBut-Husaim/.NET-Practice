WScript.Echo("start");

powerManager = new ActiveXObject("UnmanagedCode.Example.PowerManagerCom");
wakeTime = powerManager.GetLastWakeTime();

WScript.Echo("Wake time: " + wakeTime);