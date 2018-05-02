using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class DllAdapter {

    [DllImport("ThesisDll", EntryPoint = "kep2car")]
    public static unsafe extern void Kep2Car(double* kep, double* car);

    [DllImport("ThesisDll", EntryPoint = "prop_Keplerian_motion")]
    public static unsafe extern void PropKeplerianMotion(double* in_par, double dt, double* out_par);

    public static double T = 0d;

    //TODO Eliminare
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

    }

    //From TLE Object to Cartesian coordinates position and velocity
    public static ObjectState Kep2CarAdapter(TLEObject data)
    {
        double[] kep = { data.in_sma, data.in_ecc, data.in_inc * Constants.DEGREE, data.in_raan * Constants.DEGREE, data.in_m * Constants.DEGREE };
        double[] car = new double[Constants.NDIM];

        unsafe
        {
            fixed (double* kep_pointer = &kep[0], car_pointer = &car[0]) {

                Kep2Car(kep_pointer, car_pointer);

            }
        }

        ObjectState state = new ObjectState(car);

        return state;
    }

    public static ObjectState KeplerianMotionAdapter(ObjectState initial_state, TLEObject obj)
    {
        //double T = 2 * Constants.PI * Math.Sqrt((obj.in_sma * obj.in_sma * obj.in_sma) / Constants.MU_EARTH);

        double[] car_in = { initial_state.positionX, initial_state.positionY, initial_state.positionZ, initial_state.velocityX, initial_state.velocityY, initial_state.velocityZ };
        double[] car_out = new double[Constants.NDIM];

        unsafe
        {
            fixed (double* in_car = &car_in[0], out_car = &car_out[0])
            {

                PropKeplerianMotion(in_car, T, out_car);

            }
        }

        //Debug.Log(car_out[0] + " " + car_out[1] + " " + car_out[2] + " " + car_out[3] + " " + car_out[4] + " " + car_out[5]);

        return new ObjectState(car_out);
    }

    public static ObjectState KeplerianMotionAdapter(ObjectState initial_state, TLESemiObj obj)
    {
        //double T = 2 * Constants.PI * Math.Sqrt((obj.in_sma * obj.in_sma * obj.in_sma) / Constants.MU_EARTH);

        double[] car_in = { initial_state.positionX, initial_state.positionY, initial_state.positionZ, initial_state.velocityX, initial_state.velocityY, initial_state.velocityZ };
        double[] car_out = new double[Constants.NDIM];

        unsafe
        {
            fixed (double* in_car = &car_in[0], out_car = &car_out[0])
            {

                PropKeplerianMotion(in_car, T, out_car);

            }
        }

        //Debug.Log(car_out[0] + " " + car_out[1] + " " + car_out[2] + " " + car_out[3] + " " + car_out[4] + " " + car_out[5]);

        return new ObjectState(car_out);
    }
}
