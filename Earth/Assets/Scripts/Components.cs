using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SatelliteData
{
    public string name;
    public string registry_country;
    public string operator_country;
    public string operator_owner;
    public string users;
    public string purpose;
    public string detailed_purpose;
    public string orbit_class;
    public string orbit_type;
    public float longitude;
    public float perigee;
    public float apogee;
    public double eccentricity;
    public float inclinator;
    public string period;
    public float launch_mass;
    public string dry_mass;
    public string power;
    public string date_of_launch;
    public string expected_lifetime;
    public string contractor;
    public string contractor_country;
    public string launch_site;
    public string launch_vehicle;
    public string cospar;
    public string norad;
    public string comments;
    public string orbital_data_source;
    public string source;
    public string source_1;
    public string source_2;
    public string source_3;
    public string source_4;
    public string source_5;
};

public struct TLEObject
{
    public string norad;
    public double in_per;
    public double in_apo;
    public double in_inc;
    public double in_raan;
    public double in_arg;
    public double in_m;
    public double in_sma;
    public double in_ecc;
};

public struct TLESemiObj
{
    public double in_per;
    public double in_apo;
    public double in_inc;
    public double in_raan;
    public double in_arg;
    public double in_m;
    public double in_sma;
    public double in_ecc;

    public TLESemiObj(TLEObject data)
    {
        in_per = data.in_per;
        in_apo = data.in_apo;
        in_inc = data.in_inc;
        in_raan = data.in_raan;
        in_arg = data.in_arg;
        in_m = data.in_m;
        in_sma = data.in_sma;
        in_ecc = data.in_ecc;
    }
};

public struct ObjectState
{
    public double positionX;
    public double positionY;
    public double positionZ;

    public double velocityX;
    public double velocityY;
    public double velocityZ;

    public ObjectState(double[] data)
    {
        positionX = data[0];
        positionY = data[1];
        positionZ = data[2];

        velocityX = data[3];
        velocityY = data[4];
        velocityZ = data[5];
    }

    public Vector3 PositionToVector3(float scale_factor)
    {
        return new Vector3((float)positionX / scale_factor, (float)positionY / scale_factor, (float)positionZ / scale_factor);
    }
};