/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Utilities {
	using UnityEditor.Build;
    public class PedometerEditor : IOrderedCallback
    {
		int IOrderedCallback.callbackOrder { get; } = 0;

        private const string
		MotionUsageKey = @"NSMotionUsageDescription",
		MotionUsageDescription = @"Allow this app to use the pedometer."; // Change this as necessary
    }
}