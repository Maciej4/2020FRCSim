using System.Collections.Generic;

public class UnityPacket
{
    public double heartbeat = 0.0;
    public int robotMode = 0;
    public List<Hardware> hardware = new List<Hardware>();
    public List<string> hardwareString = new List<string>();
}
