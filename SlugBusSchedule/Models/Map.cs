using System;
/**Use to calculate GPS info to the nearest bus stops */

public class Map
{
    //Divide UCSC campus to 48 areas, assign each area a bus stop
    private static string a1 = "WesternDr";
    private static string a2 = "WesternDr";
    private static string a3 = "WesternDr";
    private static string a4 = "WesternDr";
    private static string a5 = "WesternDr";
    private static string a6 = "Base";
    private static string b1 = "WesternDr";
    private static string b2 = "WesternDr";
    private static string b3 = "WesternDr";
    private static string b4 = "WesternDr";
    private static string b5 = "WesternDr";
    private static string b6 = "Base";
    private static string c1 = "Oakes";
    private static string c2 = "Oakes";
    private static string c3 = "WesternDr";
    private static string c4 = "Village";
    private static string c5 = "Village";
    private static string c6 = "Village";
    private static string d1 = "Oakes";
    private static string d2 = "Oakes";
    private static string d3 = "Oakes";
    private static string d4 = "EastRemote";
    private static string d5 = "EastRemote";
    private static string d6 = "EastRemote";
    private static string e1 = "Oakes";
    private static string e2 = "PorterRachelCarson";
    private static string e3 = "PorterRachelCarson";
    private static string e4 = "EastRemote";
    private static string e5 = "EastRemote";
    private static string e6 = "EastRemote";
    private static string f1 = "PorterRachelCarson";
    private static string f2 = "PorterRachelCarson";
    private static string f3 = "PorterRachelCarson";
    private static string f4 = "BookStore";
    private static string f5 = "BookStore";
    private static string f6 = "BookStore";
    private static string g1 = "Kresge";
    private static string g2 = "Kresge";
    private static string g3 = "ScienceHill";
    private static string g4 = "BookStore";
    private static string g5 = "BookStore";
    private static string g6 = "BookStore";
    private static string h1 = "Kresge";
    private static string h2 = "Kresge";
    private static string h3 = "ScienceHill";
    private static string h4 = "NineTen";
    private static string h5 = "CrownMerill";
    private static string h6 = "CrownMerill";

    //latitude and longitude of reference lines on the map
    private static double lata = 36.977416;
    private static double latb = 36.98375;
    private static double latc = 36.987914;
    private static double latd = 36.991319;
    private static double late = 36.993035;
    private static double latf = 36.995066;
    private static double latg = 36.998631;
    private static double lon1 = -122.066329;
    private static double lon2 = -122.063748;
    private static double lon3 = -122.060331;
    private static double lon4 = -122.05626;
    private static double lon5 = -122.05615;
    public static string findBusStop(double lat, double lon)
    {
        string nearestBusStop="";
        string latSymbol;
        //Decide latitude interval
        if (lat < lata) 
        {
            latSymbol="a";
        }else if (lata <= lat && lat < latb)
        {
             latSymbol="b";
        }else if (latb <= lat && lat < latc)
        {
             latSymbol="c";
        }else if (latc <= lat && lat < latd)
        {
             latSymbol="d";
        }else if (latd <= lat && lat < late)
        {
             latSymbol="e";
        }else if (late <= lat && lat < latf)
        {
             latSymbol="f";
        }else if (latf <= lat && lat < latg)
        {
             latSymbol="g";
        }else
        {
             latSymbol="h";
        }
        //Decide longitude
        switch (latSymbol)
        {
            case "a":
                if (lon < lon1)
                {
                    nearestBusStop = a1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = a2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = a3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = a4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = a5;
                }else
                {
                    nearestBusStop = a6;
                }
                break;
            case "b":
                if (lon < lon1)
                {
                    nearestBusStop = b1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = b2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = b3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = b4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = b5;
                }else
                {
                    nearestBusStop = b6;
                }
                break;
            case "c":
                if (lon < lon1)
                {
                    nearestBusStop = c1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = c2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = c3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = c4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = c5;
                }else
                {
                    nearestBusStop = c6;
                }
                break;
            case "d":
                if (lon < lon1)
                {
                    nearestBusStop = d1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = d2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = d3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = d4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = d5;
                }else
                {
                    nearestBusStop = d6;
                }
                break;
            case "e":
                if (lon < lon1)
                {
                    nearestBusStop = e1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = e2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = e3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = e4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = e5;
                }else
                {
                    nearestBusStop = e6;
                }
                break;
            case "f":
                if (lon < lon1)
                {
                    nearestBusStop = f1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = f2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = f3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = f4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = f5;
                }else
                {
                    nearestBusStop = f6;
                }
                break;
            case "g":
                if (lon < lon1)
                {
                    nearestBusStop = g1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = g2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = g3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = g4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = g5;
                }else
                {
                    nearestBusStop = g6;
                }
                break;
            case "h":
                if (lon < lon1)
                {
                    nearestBusStop = h1;
                }else if (lon1 <= lon && lon < lon2)
                {
                    nearestBusStop = h2;
                }else if (lon2 <= lon && lon < lon3)  
                {
                    nearestBusStop = h3;
                }else if (lon3 <= lon && lon < lon4)
                {
                    nearestBusStop = h4;
                }else if (lon4 <= lon && lon < lon5)
                {
                    nearestBusStop = h5;
                }else
                {
                    nearestBusStop = h6;
                }
                break;
        }
            
        Console.WriteLine("\n\n\n\n\n"+nearestBusStop+"\n\n\n\n\n");
        return nearestBusStop;
    }

      

}