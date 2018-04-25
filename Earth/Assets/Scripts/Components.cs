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