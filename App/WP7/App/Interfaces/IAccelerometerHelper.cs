using System;
using TexasHoldemCalculator.Interfaces.Model;

namespace TexasHoldemCalculator.Interfaces
{
    public interface IAccelerometerHelper
    {
        /// <summary>
        /// New raw and processed accelerometer data available event.
        /// Fires every 20ms.
        /// </summary>
        event EventHandler<AccelerometerHelperReadingEventArgs> ReadingChanged;

        /// <summary>
        /// Singleton instance of the Accelerometer Helper class
        /// </summary>
        //public static AccelerometerHelper Instance
        //{
        //    get
        //    {
        //        if (_singletonInstance == null)
        //        {
        //            lock (_syncRoot)
        //            {
        //                if (_singletonInstance == null)
        //                {
        //                    _singletonInstance = new AccelerometerHelper();
        //                }
        //            }
        //        }
        //        return _singletonInstance;
        //    }
        //}
        /// <summary>
        /// True when the device is "stable" (no movement for about 0.5 sec)
        /// </summary>
        bool DeviceStable { get; }

        /// <summary>
        /// Persistent data (calibration of accelerometer)
        /// </summary>
        Simple3DVector ZeroAccelerationCalibrationOffset { get; }

        /// <summary>
        /// Accelerometer is not present on device 
        /// </summary>
        bool NoAccelerometer { get; }

        /// <summary>
        /// Accelerometer is active and reading value when true
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Release sensor resource if not already done
        /// </summary>
        void Dispose();

        /// <summary>
        /// Indicate that the calibration of the sensor would work along a particular set of axis
        /// because the device is "stable enough" or not inclined beyond reasonable
        /// </summary>
        /// <param name="xAxis">check stability on X axis if true</param>
        /// <param name="yAxis">check stability on X axis if true</param>
        /// <returns>true if all of the axis checked were "stable enough" or not too inclined</returns>
        bool CanCalibrate(bool xAxis, bool yAxis);

        /// <summary>
        /// Calibrates the accelerometer on X and / or Y axis and save data to isolated storage.
        /// </summary>
        /// <param name="xAxis">calibrates X axis if true</param>
        /// <param name="yAxis">calibrates Y axis if true</param>
        /// <returns>true if succeeds</returns>
        bool Calibrate(bool xAxis, bool yAxis);
    }
}