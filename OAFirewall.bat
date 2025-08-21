netsh http add urlacl url="http://+:8085/ask/" user=everyone
netsh http add urlacl url="http://+:8085/ask/" user=iedereen

netsh advfirewall firewall delete rule name="EasyFactuurBOT"
::netsh advfirewall firewall add rule name="EasyFactuurBOT" dir=in action=allow protocol=TCP localport=8085 remoteip=37.97.195.81
netsh advfirewall firewall add rule name="EasyFactuurBOT" dir=in action=allow protocol=TCP localport=8085 


echo press anykey...
pause