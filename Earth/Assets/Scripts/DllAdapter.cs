using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DllAdapter : MonoBehaviour {

    [DllImport("ThesisDll", EntryPoint = "kep2car")]
    public static unsafe extern void Kep2Car(double* kep, double* car);

    [DllImport("ThesisDll", EntryPoint = "prop_Keplerian_motion")]
    public static unsafe extern void PropKeplerianMotion(double* in_par, double dt, double* out_par);

    //TODO Eliminare e togliere monobehav
    public void Start()
    {
        double[] state = new double[6] { 15d, 10d, 1234d, 156d, 172d, 18d };
        double[] out_state = new double[6]; // { 1d, 1d, 1d, 1d, 1d, 1d };

        unsafe
        {
            fixed(double* state_in = &state[0], state_out = &out_state[0])
            {
                //PropKeplerianMotion(state_in, 0.2f, state_out);
                Kep2Car(state_in, state_out);
            }
        }

        foreach(double el in out_state)
        {
            Debug.Log(el+" ");
        }
    }

}
