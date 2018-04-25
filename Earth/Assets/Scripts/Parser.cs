using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class Parser : MonoBehaviour {

    //TODO eliminare
    public void Start()
    {
        //ReadAllSatellites("./Assets/Resources/sat_data_clean.txt");
        //ReadAllTLE("./Assets/Resources/TLE_Objects_16_03_2018_clean.txt");
    }

    public static List<SatelliteData> ReadAllSatellites(string filename)
    {
        List<SatelliteData> satellites = new List<SatelliteData>();

        StreamReader reader = File.OpenText(filename);

        string line = reader.ReadLine(); //Discard header line 

        while((line = reader.ReadLine()) != null)
        {
            satellites.Add(ParseOneSatLine(line));
        }

        return satellites;
    }

    private static SatelliteData ParseOneSatLine(string line)
    {
        string[] items = line.Split('\t');
        SatelliteData data;

        data.name = items[0];
        data.registry_country = items[1];
        data.operator_country = items[2];
        data.operator_owner = items[3];
        data.users = items[4];
        data.purpose = items[5];
        data.detailed_purpose = items[6];
        data.orbit_class = items[7];
        data.orbit_type = items[8];
        data.longitude = SanitizeFloat(items[9], 0f);
        data.perigee = SanitizeFloat(items[10], 0f);
        data.apogee = SanitizeFloat(items[11], 0f);
        data.eccentricity = SanitizeDouble(items[12], 0d);
        data.inclinator = SanitizeFloat(items[13], 0f);
        data.period = items[14];
        data.launch_mass = SanitizeFloat(items[15], 0f);
        data.dry_mass = items[16]; 
        data.power = items[17]; 
        data.date_of_launch = items[18];
        data.expected_lifetime = items[19];
        data.contractor = items[20];
        data.contractor_country = items[21];
        data.launch_site = items[22];
        data.launch_vehicle = items[23];
        data.cospar = items[24];
        data.norad = items[25];
        data.comments = items[26];

        data.orbital_data_source = items[27];
        data.source = items[28];
        data.source_1 = items[29];
        data.source_2 = items[30];
        data.source_3 = items[31];
        data.source_4 = items[32];
        data.source_5 = items[33];

        return data;
    }

    private static float SanitizeFloat(string item, float default_value)
    {
        return item != "" ? float.Parse(item, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture) : default_value;
    }

    private static double SanitizeDouble(string item, double default_value)
    {
        if(item.Equals(""))
        {
            return default_value;
        }

        //il parse di double dà errore parsando zero da queste stringhe
        if (item.Equals("0.00E"))
        {
            return 0d;
        }

        if (float.Parse(item, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture) != 0f)
        {
            return double.Parse(item);
        }else
        {
            return 0d;
        }

    }

    public static List<TLEObject> ReadAllTLE(string filename)
    {
        List<TLEObject> objects = new List<TLEObject>();
    
        StreamReader reader = File.OpenText(filename);

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            objects.Add(ParseOneTLELine(line));
        }

        return objects;
    }

    private static TLEObject ParseOneTLELine(string line)
    {
        string[] items = line.Split('\t');
        TLEObject data;

        data.norad = items[0];
        data.in_per = double.Parse(items[1]) + Constants.R_EARTH;
        data.in_apo = double.Parse(items[2]) + Constants.R_EARTH;
        data.in_inc = double.Parse(items[3]);
        data.in_raan = double.Parse(items[4]);
        data.in_arg = double.Parse(items[5]);
        data.in_m = double.Parse(items[6]);

        data.in_sma = 0.5d * (data.in_per + data.in_apo);
        data.in_ecc = (data.in_apo - data.in_per) / (data.in_apo + data.in_per);

        return data;
    }
}
