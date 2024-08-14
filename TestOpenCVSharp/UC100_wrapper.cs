//****************************************
// UCxx motion control API C# wrapper.
// Made by CNCdrive Kft, www.cncdrive.com
//****************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

public class UC100
{
    /// <summary>
    /// Scans and lists all connected UC devices.
    /// </summary>
    /// <param name="DevicesCount">Returns the number of available devices.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int ListDevices(ref int DevicesCount);

    /// <summary>
    /// Returns the device properties defined by the boardID parameter. If the BoardID is lower or equals 0 then it is demo mode.
    /// </summary>
    /// <param name="BoardID">The ID of the board (Listdevices list number).</param>
    /// <param name="Type">Device type (DeviceType enum)</param>
    /// <param name="SerialNumber">Returns the serial number of the controller as an integer value. (Convert to hexadecimal to get the serial number in the correct format.)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int DeviceInfo(int BoardID, ref int Type, ref int SerialNumber);

    /// <summary>
    /// Opens the connection with the device defined by the BoardID parameter. If BoardID lower or equal 0 then demo mode, otherwise device open.
    /// </summary>
    /// <param name="BoardID">The ID of the board (Listdevices list number).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Open(int BoardID);

    /// <summary>
    /// Closes the connection with the device.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Close();

    /// <summary>
    /// Sets the kernel frequency.
    /// </summary>
    /// <param name="Rate">0->100kHz, 1->50kHz, 2->25kHz, 3->200kHz, 4->400kHz</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetStepRate(int Rate);

    /// <summary>
    /// Reads the kernel frequency.
    /// </summary>
    /// <param name="Rate">0->100kHz, 1->50kHz, 2->25kHz, 3->200kHz, 4->400kHz</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetStepRate(ref int Rate);

    /// <summary>
    /// Reads the input pins states.
    /// </summary>
    /// <param name="Inp">One bit per pin.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetInput(ref long Inp);

    /// <summary>
    /// Reads the output pins states.
    /// </summary>
    /// <param name="Out">One bit per pin.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetOutput(ref long Out);

    /// <summary>
    /// Writes the output pins states.
    /// </summary>
    /// <param name="Out">One bit per pin.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetOutput(long Out);

    /// <summary>
    /// Sets the MPG parameters. (MPGx=0 -> MPG1, MPGx=1 -> MPG2)
    /// </summary>
    /// <param name="MPGx">The number of the MPG.</param>
    /// <param name="MPGCount">The MPG count per turn value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetMPGCount(int MPGx, int MPGCount);

    /// <summary>
    /// Gets the MPG parameters. (MPGx=0 -> MPG1, MPGx=1 -> MPG2)
    /// </summary>
    /// <param name="MPGx">The number of the MPG.</param>
    /// <param name="MPGCount">The MPG count per turn value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetMPGCount(int MPGx, ref int MPGCount);

    /// <summary>
    /// Sets the charge pump settings.
    /// </summary>
    /// <param name="Pin1">Output pin of charge pump1.</param>
    /// <param name="PinNeg1">Inverts charge pump1. output.</param>
    /// <param name="Pin2">Output pin of charge pump2.</param>
    /// <param name="PinNeg2">Inverts charge pump2. output.</param>
    /// <param name="ChargeAlwaysOn">Charge pump active also in reset.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetChargePumpSetting(int Pin1, bool PinNeg1, int Pin2, bool PinNeg2, [MarshalAs(UnmanagedType.I1)] bool ChargeAlwaysOn);

    /// <summary>
    /// Gets the charge pump settings.
    /// </summary>
    /// <param name="Pin1">Output pin of charge pump1.</param>
    /// <param name="PinNeg1">Inverts charge pump1. output.</param>
    /// <param name="Pin2">Output pin of charge pump2.</param>
    /// <param name="PinNeg2">Inverts charge pump2. output.</param>
    /// <param name="ChargeAlwaysOn">Charge pump active also in reset.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetChargePumpSetting(ref int Pin1, [MarshalAs(UnmanagedType.I1)] ref bool PinNeg1, ref int Pin2, [MarshalAs(UnmanagedType.I1)] ref bool PinNeg2, [MarshalAs(UnmanagedType.I1)] ref bool ChargeAlwaysOn);

    /// <summary>
    /// Sets the debounce (inputs digital low pass filter).
    /// </summary> 
    /// <param name="Debounce">General inputs debounce filter time constant(value*1msec).</param>
    /// <param name="THCDebounce">THC inputs debounce filter time constant(value*1msec).</param>
    /// <param name="HomeDebounce">Home inputs debounce filter time constant(value*1msec).</param>
    /// <param name="Probe1Debounce">Probe1 inputs debounce filter time constant(value*1msec).</param>
    /// <param name="Probe2Debounce">Probe2 inputs debounce filter time constant(value*1msec).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetDebounce(int Debounce, int THCDebounce, int HomeDebounce, int Probe1Debounce, int Probe2Debounce);

    /// <summary>
    /// Gets the debounce (inputs digital low pass filter).
    /// </summary>
    /// <param name="Debounce">General inputs debounce filter time constant(value*1msec).</param>
    /// <param name="THCDebounce">THC inputs debounce filter time constant(value*1msec).</param>
    /// <param name="HomeDebounce">Home inputs debounce filter time constant(value*1msec).</param>
    /// <param name="Probe1Debounce">Probe1 inputs debounce filter time constant(value*1msec).</param>
    /// <param name="Probe2Debounce">Probe2 inputs debounce filter time constant(value*1msec).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetDebounce(ref int Debounce, ref int THCDebounce, ref int HomeDebounce, ref int Probe1Debounce, ref int Probe2Debounce);

    /// <summary>
    /// Sets the e-stop input pin.
    /// </summary>
    /// <param name="Pin">Input pin of e-stop.</param>
    /// <param name="PinNeg">Inverts the e-stop input.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetEstopSetting(int Pin, [MarshalAs(UnmanagedType.I1)] bool PinNeg);

    /// <summary>
    /// Gets the e-stop input pin.
    /// </summary>
    /// <param name="Pin">Input pin of e-stop.</param>
    /// <param name="PinNeg">Inverts the e-stop input.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetEstopSetting(ref int Pin, [MarshalAs(UnmanagedType.I1)] ref bool PinNeg);

    /// <summary>
    /// Stop command which stops motion at the end of each movement segment.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int StopEndOfSegment();

    /// <summary>
    /// Stop command which stops motion at the end of each movement.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int StopWithDeccel();

    /// <summary>
    /// Stop command which makes an instant stop (e-stop like stop).
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Stop();

    /// <summary>
    /// Sets the reset condition.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetEstopState();

    /// <summary>
    /// Sets the path trajectory (Constant velocity) parameters.
    /// </summary>
    /// <param name="_TrajParam">The trajectory planner parameters struct.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetTrajParam([MarshalAs(UnmanagedType.Struct)] ref TrajParam _TrajParam);

    /// <summary>
    /// Gets the path trajectory (Constant velocity) parameters.
    /// </summary>
    /// <param name="_TrajParam">The trajectory planner parameters struct.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetTrajParam([MarshalAs(UnmanagedType.Struct)] ref TrajParam _TrajParam);

    /// <summary>
    /// Sets the axes parameters.
    /// </summary>
    /// <param name="_AxisSetting">AxisSettings struct with axis parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAxisSetting([MarshalAs(UnmanagedType.Struct)] ref AxisSetting _AxisSetting);

    /// <summary>
    /// Gets the axes parameters.
    /// </summary>
    /// <param name="_AxisSetting">AxisSettings struct with axis parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetAxisSetting([MarshalAs(UnmanagedType.Struct)] ref AxisSetting _AxisSetting);

    /// <summary>
    /// Sets the position (coordinate counter) of the axes in Units.
    /// </summary>
    /// <param name="Xpos">X axis position in Units.</param>
    /// <param name="Ypos">Y axis position in Units.</param>
    /// <param name="Zpos">Z axis position in Units.</param>
    /// <param name="Apos">A axis position in Units.</param>
    /// <param name="Bpos">B axis position in Units.</param>
    /// <param name="Cpos">C axis position in Units.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAxisPosition(double Xpos, double Ypos, double Zpos, double Apos, double Bpos, double Cpos);

    /// <summary>
    /// Gets the position (coordinate counter) of the axes in Units.
    /// </summary>
    /// <param name="Xpos">X axis position in Units.</param>
    /// <param name="Ypos">Y axis position in Units.</param>
    /// <param name="Zpos">Z axis position in Units.</param>
    /// <param name="Apos">A axis position in Units.</param>
    /// <param name="Bpos">B axis position in Units.</param>
    /// <param name="Cpos">C axis position in Units.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAxisPosition(ref double Xpos, ref double Ypos, ref double Zpos, ref double Apos, ref double Bpos, ref double Cpos);

    /// <summary>
    /// Gets the DTG (distance to go) of the axes in Units.
    /// </summary>
    /// <param name="Xpos">X axis distance to go in Units.</param>
    /// <param name="Ypos">Y axis distance to go in Units.</param>
    /// <param name="Zpos">Z axis distance to go in Units.</param>
    /// <param name="Apos">A axis distance to go in Units.</param>
    /// <param name="Bpos">B axis distance to go in Units.</param>
    /// <param name="Cpos">C axis distance to go in Units.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAxisDTG(ref double Xpos, ref double Ypos, ref double Zpos, ref double Apos, ref double Bpos, ref double Cpos);

    /// <summary>
    /// Sets the axis position in Steps.
    /// </summary>
    /// <param name="Xpos">X axis position in Steps.</param>
    /// <param name="Ypos">Y axis position in Steps.</param>
    /// <param name="Zpos">Z axis position in Steps.</param>
    /// <param name="Apos">A axis position in Steps.</param>
    /// <param name="Bpos">B axis position in Steps.</param>
    /// <param name="Cpos">C axis position in Steps.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAxisIndex(int Xpos, int Ypos, int Zpos, int Apos, int Bpos, int Cpos);

    /// <summary>
    /// Gets the axis position in Steps.
    /// </summary>
    /// <param name="Xpos">X axis position in Steps.</param>
    /// <param name="Ypos">Y axis position in Steps.</param>
    /// <param name="Zpos">Z axis position in Steps.</param>
    /// <param name="Apos">A axis position in Steps.</param>
    /// <param name="Bpos">B axis position in Steps.</param>
    /// <param name="Cpos">C axis position in Steps.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAxisIndex(ref int Xpos, ref int Ypos, ref int Zpos, ref int Apos, ref int Bpos, ref int Cpos);

    /// <summary>
    /// Adds one linear movement to the motion buffer for execution.
    /// </summary>
    /// <param name="x">X axis new position in Units.</param>
    /// <param name="y">Y axis new position in Units.</param>
    /// <param name="z">Z axis new position in Units.</param>
    /// <param name="a">A axis new position in Units.</param>
    /// <param name="b">B axis new position in Units.</param>
    /// <param name="c">C axis new position in Units.</param>
    /// <param name="Feed">Path feedrate in Units/second.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddLinearMove(double x, double y, double z, double a, double b, double c, double Feed, int ID);

    /// <summary>
    /// Adds one circular movement to the motion buffer for execution. Plane 0=XY, 1=XZ, 2=YZ. NextID is the ID of the next command for proper stop and restart on the end of the arc.
    /// </summary>
    /// <param name="x">X axis new position in Units.</param>
    /// <param name="y">Y axis new position in Units.</param>
    /// <param name="z">Z axis new position in Units.</param>
    /// <param name="rx">Arc center X coordinate in Units.</param>
    /// <param name="ry">Arc center Y coordinate in Units.</param>
    /// <param name="rz">Arc center Z coordinate in Units.</param>
    /// <param name="Dir">Arc direction (CW/CCW)</param>
    /// <param name="Feed">Path feedrate in Units/second.</param>
    /// <param name="Plane">Arc plane (0=XY, 1=XZ, 2=YZ)</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <param name="NextID">Identifier of the next command.(Required for proper stop and restart at the end of the arc.)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddCircularMove(double x, double y, double z, double rx, double ry, double rz, bool Dir, double Feed, int Plane, int ID, int NextID);

    /// <summary>
    /// Adds a dwell (wait) command to the motion buffer for execution.
    /// </summary>
    /// <param name="Time">Dwell time in msec.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddDwell(double Time, int ID);

    /// <summary>
    /// Adds a dummy move command to the motion buffer for execution. (no move, but the returned ID changes).
    /// </summary>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddDummyMove(int ID);

    /// <summary>
    /// Sets the spindle parameters.
    /// </summary>
    /// <param name="_SPSetting">Spindle parameters struct.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetSpindleSetting([MarshalAs(UnmanagedType.Struct)] ref SPSetting _SPSetting);

    /// <summary>
    /// Gets the spindle parameters.
    /// </summary>
    /// <param name="_SPSetting">Spindle parameters struct.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetSpindleSetting([MarshalAs(UnmanagedType.Struct)] ref SPSetting _SPSetting);

    /// <summary>
    /// Turns the spindle on.
    /// </summary>
    /// <param name="CCW">Direction of rotation CW/CCW.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SpindleOn([MarshalAs(UnmanagedType.I1)] bool CCW);

    /// <summary>
    /// Sets the spindle speed.
    /// </summary>
    /// <param name="Speed">Spindle speed parameter.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetSpindleSpeed(double Speed);

    /// <summary>
    /// Gets the spindle speed.
    /// </summary>
    /// <param name="Speed">Spindle speed parameter.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetSpindleSpeed(ref double Speed);

    /// <summary>
    /// Turns the spindle off.
    /// </summary>
    /// <returns></returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SpindleOff();

    /// <summary>
    /// Sets the index prescaler and pin of the spindle index sensor.
    /// </summary>
    /// <param name="Pin">Input pin of spindle index.</param>
    /// <param name="Prescale">Index pulse count per spindle turn.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetIndexSetting(int Pin, double Prescale);

    /// <summary>
    /// Gets the index prescaler and pin of the spindle index sensor.
    /// </summary>
    /// <param name="Pin">Input pin of spindle index.</param>
    /// <param name="Prescale">Index pulse count per spindle turn.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetIndexSetting(ref int Pin, ref double Prescale);

    /// <summary>
    /// Sets the THC (torch height control for plasma) settings.
    /// </summary>
    /// <param name="_THCSetting">THC settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetTHCSetting([MarshalAs(UnmanagedType.Struct)] ref THCSetting _THCSetting);

    /// <summary>
    /// Gets the THC (torch height control for plasma) settings.
    /// </summary>
    /// <param name="_THCSetting">THC settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetTHCSetting([MarshalAs(UnmanagedType.Struct)] ref THCSetting _THCSetting);

    /// <summary>
    /// Nulls the THC position correction value.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int THCNullCorrection();

    /// <summary>
    /// Gets the THC position correction value.
    /// </summary>
    /// <param name="THCCorr">The THC correction distance value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetTHCCorrection(ref double THCCorr);

    /// <summary>
    /// Enables the THC control
    /// </summary>
    /// <param name="Enable">Enable/Disable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int THCEnable([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// Gets the status structure with the status parameters.
    /// </summary>
    /// <param name="SStat">The status structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetStatus([MarshalAs(UnmanagedType.Struct)] ref Stat SStat);

    /// <summary>
    /// Homes one axis.
    /// </summary>
    /// <param name="Axis">The axis to be homed.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="SpeedUp">The movement speed when moving towards the home sensor.</param>
    /// <param name="SpeedDown">The movement speed when moving off the home sensor.</param>
    /// <param name="Dir">The direction of homing.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int HomeOn(int Axis, double SpeedUp, double SpeedDown, [MarshalAs(UnmanagedType.I1)] bool Dir);

    /// <summary>
    /// Switches the jog on on one axis. Multiple calls on different axis is possible.
    /// </summary>
    /// <param name="Axis">The axis to be jogged.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="Dir">The jog direction.</param>
    /// <param name="MaxSpeed">The speed of jog.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JogOn(int Axis, [MarshalAs(UnmanagedType.I1)] bool Dir, [MarshalAs(UnmanagedType.I1)] bool MaxSpeed);

    /// <summary>
    /// Switches the jog off on one axis.
    /// </summary>
    /// <param name="Axis">The axis to stop jogging.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C, 6=all)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JogOff(int Axis);

    /// <summary>
    /// Sets the outputs to offline mode. (inhibits the outputs)
    /// </summary>
    /// <param name="State">Enable/Disable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetOffline([MarshalAs(UnmanagedType.I1)] bool State);

    /// <summary>
    /// Gets the offline state.
    /// </summary>
    /// <param name="State">Enabled/Disabled.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetOffline([MarshalAs(UnmanagedType.I1)] ref bool State);

    /// <summary>
    /// Sets the probe sensors settings.
    /// </summary>
    /// <param name="Pin1">Probe1. input pin.</param>
    /// <param name="Pin1Neg">Inverts the probe1. input.</param>
    /// <param name="Pin2">Probe2. input pin.</param>
    /// <param name="Pin2Neg">Inverts the probe 2. input.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetProbeSetting(int Pin1, [MarshalAs(UnmanagedType.I1)] bool Pin1Neg, int Pin2, [MarshalAs(UnmanagedType.I1)] bool Pin2Neg);

    /// <summary>
    /// Gets the probe sensors settings.
    /// </summary>
    /// <param name="Pin1">Probe1. input pin.</param>
    /// <param name="Pin1Neg">Inverts the probe1. input.</param>
    /// <param name="Pin2">Probe2. input pin.</param>
    /// <param name="Pin2Neg">Inverts the probe 2. input.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetProbeSetting(ref int Pin1, [MarshalAs(UnmanagedType.I1)] ref bool Pin1Neg, ref int Pin2, [MarshalAs(UnmanagedType.I1)] ref bool Pin2Neg);

    /// <summary>
    /// Starts a probing operation.
    /// </summary>
    /// <param name="Axis">The axis to probe.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="ProbePos">The end position of the probing operation.</param>
    /// <param name="Speed">The probing speed.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ProbeStart(int Axis, double ProbePos, double Speed, int ID);

    /// <summary>
    /// Stops a probing operation.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ProbeStop();

    /// <summary>
    /// Enables the softlimits.
    /// </summary>
    /// <param name="SoftLimitEnable">Enable/Disable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetSoftLimit([MarshalAs(UnmanagedType.I1)] bool SoftLimitEnable);

    /// <summary>
    /// Gets the softlimit enable.
    /// </summary>
    /// <param name="SoftLimitEnable"></param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetSoftLimit([MarshalAs(UnmanagedType.I1)] ref bool SoftLimitEnable);

    /// <summary>
    /// Overrides the limit input pins if any of them are active. Not set and automatically cleared when they are not active and when they all become inactive.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int LimitOverRide();

    /// <summary>
    /// Gets the API version datas.
    /// </summary>
    /// <param name="Version">Version structure</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetVersio([MarshalAs(UnmanagedType.Struct)] ref Versio Version);

    /// <summary>
    /// Sets the FRO (feedrate override).
    /// </summary>
    /// <param name="FRO">FRO percentage (0-300%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetFRO(double FRO);

    /// <summary>
    /// Gets the FRO (feedrate override).
    /// </summary>
    /// <param name="FRO">FRO percentage (0-300%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetFRO(ref double FRO);

    /// <summary>
    /// Sets the SRO (spindle rate override).
    /// </summary>
    /// <param name="SRO">SRO percentage (0-300%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetSRO(double SRO);

    /// <summary>
    /// Gets the SRO (spindle rate override).
    /// </summary>
    /// <param name="SRO">SRO percentage (0-300%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetSRO(ref double SRO);

    /// <summary>
    /// Sets the JRO (jog rate override).
    /// </summary>
    /// <param name="JRO">JRO percentage (0-100%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetJRO(double JRO);

    /// <summary>
    /// Gets the JRO (jog rate override).
    /// </summary>
    /// <param name="JRO">JRO percentage (0-100%).</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetJRO(ref double JRO);

    /// <summary>
    /// Switches the MPG jog on.
    /// </summary>
    /// <param name="Axis">Axis to jog with the MPG.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="MPG">MPG number. (Currently only 1 MPG is available)</param>
    /// <param name="Mode">Mode of MPG operation.</param>
    /// <param name="Step">Steps per MPG tick.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MPGJogOn(int Axis, int MPG, int Mode, double Step);

    /// <summary>
    /// Switches the MPG jog off.
    /// </summary>
    /// <param name="MPG">MPG number. (Currently only 1 MPG is available)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MPGJogOff(int MPG);

    /// <summary>
    /// Gets the analog inputs values.
    /// </summary>
    /// <param name="AnIn1">Analog input channel 1. value.</param>
    /// <param name="AnIn2">Analog input channel 2. value.</param>
    /// <param name="AnIn3">Analog input channel 3. value.</param>
    /// <param name="AnIn4">Analog input channel 4. value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAnalogInput(ref int AnIn1, ref int AnIn2, ref int AnIn3, ref int AnIn4);

    /// <summary>
    /// Sets the analog output channel values.
    /// </summary>
    /// <param name="AnOut1">Analog output channel 1. value.</param>
    /// <param name="AnOut2">Analog output channel 2. value.</param>
    /// <param name="AnOut3">Analog output channel 3. value.</param>
    /// <param name="AnOut4">Analog output channel 4. value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAnalogOut(int AnOut1, int AnOut2, int AnOut3, int AnOut4);

    /// <summary>
    /// Gets the analog output channel values.
    /// </summary>
    /// <param name="AnOut1">Analog output channel 1. value.</param>
    /// <param name="AnOut2">Analog output channel 2. value.</param>
    /// <param name="AnOut3">Analog output channel 3. value.</param>
    /// <param name="AnOut4">Analog output channel 4. value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAnalogOut(ref int AnOut1, ref int AnOut2, ref int AnOut3, ref int AnOut4);

    /// <summary>
    /// Sets the analog channels parameters.
    /// </summary>
    /// <param name="_AnalogSetting">Analog settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAnalogSetting([MarshalAs(UnmanagedType.Struct)] ref AnalogSetting _AnalogSetting);

    /// <summary>
    /// Gets the analog channels parameters.
    /// </summary>
    /// <param name="_AnalogSetting">Analog settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetAnalogSetting([MarshalAs(UnmanagedType.Struct)] ref AnalogSetting _AnalogSetting);

    /// <summary>
    /// Adds a spindle syncronised motion (Thread cutting.) to the motion buffer for execution.
    /// </summary>
    /// <param name="x">Movement endpoint on the X axis.</param>
    /// <param name="z">Movement endpoint on the Z axis.</param>
    /// <param name="Pitch">Thread pitch.</param>
    /// <param name="Degree">The degree to plunge in.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddSyncMove(double x, double z, double Pitch, double Degree, int ID);     //Menetvágás

    /// <summary>
    /// Sets the spindle and AUX encoder and MPG settings.
    /// </summary>
    /// <param name="_EncoderSetting">The encoder settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetEncoderSetting([MarshalAs(UnmanagedType.Struct)] ref EncoderSetting _EncoderSetting);

    /// <summary>
    /// Gets the spindle and AUX encoder and MPG settings.
    /// </summary>
    /// <param name="_EncoderSetting">The encoder settings structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetEncoderSetting([MarshalAs(UnmanagedType.Struct)] ref EncoderSetting _EncoderSetting);

    /// <summary>
    /// Adds a spindle syncronised Z axis movement (rigid tapping) to the motion buffer for execution.
    /// </summary>
    /// <param name="z">The endpoint of the movement.</param>
    /// <param name="Pitch">The pitch per spindle turn.</param>
    /// <param name="Dir">Direction of the tap.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddRigidTappingMove(double z, double Pitch, [MarshalAs(UnmanagedType.I1)] bool Dir, int ID);

    /// <summary>
    /// Clears the softlimit status if it was set.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ClearSoftlimitState();

    /// <summary>
    /// Sets the feedhold and pauses the motion.
    /// </summary>
    /// <param name="State">on/off.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetFeedHold(bool State);

    /// <summary>
    /// Gets the feedhold state.
    /// </summary>
    /// <param name="State">on/off.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetFeedHold([MarshalAs(UnmanagedType.I1)] ref bool State);

    /// <summary>
    /// Adds a syncronous laser output pin PWM switching operation to the motion buffer for execution.
    /// </summary>
    /// <param name="PinDuty">The duty cycle of the output PWM.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddPinSwitch(int PinDuty, int ID);

    /// <summary>
    /// Adds a syncronous pin on/off swtiching operation to the motion buffer for execution.
    /// </summary>
    /// <param name="Output">The output pin number configured in the I/Os (value 1 to 10.)</param>
    /// <param name="Active">on/off.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddPinSwitch2(int Output, bool Active, int ID);

    /// <summary>
    /// Loads the laser image data array for later execution.
    /// </summary>
    /// <param name="_LaserPictureData">The laserpicture data array struct.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe private static extern int SetLaserPictureData([MarshalAs(UnmanagedType.Struct)] ref LaserPictureData _LaserPictureData);

    /// <summary>
    /// Starts the laser image data execution.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int StartLaserPicture();

    /// <summary>
    /// Gets the done percentage of the laser image data execution.
    /// </summary>
    /// <param name="Percent">Data executed percentage.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetLaserProgress(ref double Percent);

    /// <summary>
    /// Unloads the laser image data from the memory array.
    /// </summary>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int UnloadLaserPictureData();

    /// <summary>
    /// Sets the laser output pin.
    /// </summary>
    /// <param name="Pin">Laser output pin.</param>
    /// <param name="PinNeg">Inverts the laser output pin.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetLaserOutPin(int Pin, [MarshalAs(UnmanagedType.I1)] bool PinNeg);    //Lézer kimenet beállítása

    /// <summary>
    /// Gets the laser output pin.
    /// </summary>
    /// <param name="Pin">Laser output pin.</param>
    /// <param name="PinNeg">Inverts the laser output pin.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetLaserOutPin(ref int Pin, [MarshalAs(UnmanagedType.I1)] ref bool PinNeg); //Lézer kimenet lekérdezése

    /// <summary>
    /// Gets the debug data from the API. (For internal use only, not for developers)
    /// </summary>
    /// <param name="D1">Data1. value</param>
    /// <param name="D2">Data2. value</param>
    /// <param name="D3">Data3. value</param>
    /// <param name="D4">Data4. value</param>
    /// <param name="D5">Data5. value</param>
    /// <param name="D6">Data6. value</param>
    /// <param name="D7">Data7. value</param>
    /// <param name="D8">Data8. value</param>
    /// <param name="D9">Data9. value</param>
    /// <param name="D10">Data10. value</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetDebugData(ref double D1, ref double D2, ref double D3, ref double D4, ref double D5, ref double D6, ref double D7, ref double D8, ref double D9, ref double D10);

    /// <summary>
    /// Adds a THC on/off command to the motion buffer for execution.
    /// </summary>
    /// <param name="Enable">Enable/disable</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int AddTHCEnable([MarshalAs(UnmanagedType.I1)] bool Enable, int ID);

    /// <summary>
    /// Starts a jogging operation on an axis.
    /// </summary>
    /// <param name="Axis">The axis to be jogged.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="Dir">Direction of the jog.</param>
    /// <param name="Speed">Speed of jog.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JogOnSpeed(int Axis, [MarshalAs(UnmanagedType.I1)] bool Dir, double Speed);    //Jog indítása az Axis tengelyen (X=0,Y=1,Z=2,A=3,B=4,C=5) Dir-> irány, Speed = 1-100% jogolás sebessége

    /// <summary>
    /// Adds a relative coordinate linear movement to the motion buffer.
    /// </summary>
    /// <param name="Axis">Axis to be moved.(0=X, 1=Y, 2=Z, 3=A, 4=B, 5=C)</param>
    /// <param name="Step">Step size.</param>
    /// <param name="StepCount">The number of steps.</param>
    /// <param name="Speed">Speed of movement</param>
    /// <param name="Dir">Direction of movement.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddLinearMoveRel(int Axis, double Step, int StepCount, double Speed, [MarshalAs(UnmanagedType.I1)] bool Dir);

    /// <summary>
    /// Adds an OEM variable value change to the motion buffer.
    /// </summary>
    /// <param name="Num">The number of the OEM var.(range:0-100)</param>
    /// <param name="Val">New value of the variable.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddOEMVar(int Num, double Val, int ID);

    /// <summary>
    /// Gets the value of an OEM variable.
    /// </summary>
    /// <param name="Num">The number of the OEM var.(range:0-100)</param>
    /// <param name="Val">Returned value of the OEM var.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetOEMVar(int Num, ref double Val);

    /// <summary>
    /// Adds a variable value change to the motion buffer.
    /// </summary>
    /// <param name="Num">The number of the variable.(range:0-6000)</param>
    /// <param name="Val">New value of the variable.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddIvar(int Num, double Val, int ID);      //Belső változónak mozgásokkal szinkronozott értékadása  (Num 0-6000 lehet)

    /// <summary>
    /// Sets the value of a variable.
    /// </summary>
    /// <param name="Num">The number of the variable.(range:0-6000)</param>
    /// <param name="Val">New value of the variable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetIvar(int Num, double Val);

    /// <summary>
    /// Gets the value of a variable.
    /// </summary>
    /// <param name="Num">The number of the variable.(range:0-6000)</param>
    /// <param name="Val">Returned value of the variable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetIvar(int Num, ref double Val);

    /// <summary>
    /// Gets the reason for the last e-stop reset event.
    /// </summary>
    /// <param name="Cause">Reason for the e-stop reset. (0=no reset, 1=e-stop input, 2=limit input.)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetEstopCause(ref int Cause);

    /// <summary>
    /// Switches the THC anti dive on/off.
    /// </summary>
    /// <param name="Enable">On/off.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int THCAntiDiveEnable([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// Switches the THC arcon movement delay on/off.
    /// </summary>
    /// <param name="Enable">Enable/disable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int THCDelayEnable([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// Switches the THC anti down on/off.
    /// </summary>
    /// <param name="Enable">Enable/disable.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int THCAntiDownEnable([MarshalAs(UnmanagedType.I1)] bool Enable);      //THC AntiDown ki/bekapcsolása (nem szinkron)

    /// <summary>
    /// Adds a THC antidive on/off switching operation to the motion buffer.
    /// </summary>
    /// <param name="Enable">Enable/disable.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddTHCAntiDiveEnable([MarshalAs(UnmanagedType.I1)] bool Enable, int ID);

    /// <summary>
    /// Adds a THC arcon movement delay on/off operation to the motion buffer.
    /// </summary>
    /// <param name="Enable">Enable/disable.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddTHCDelayEnable([MarshalAs(UnmanagedType.I1)] bool Enable, int ID);

    /// <summary>
    /// Adds a THC anti down on/off operation to the motion buffer.
    /// </summary>
    /// <param name="Enable">Enable/disable.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddTHCAntiDownEnable([MarshalAs(UnmanagedType.I1)] bool Enable, int ID);  //THC AntiDown Be/kikapcsolása szinkronban a mozgásokkal

    /// <summary>
    /// Sets the THC virtual control signals
    /// </summary>
    /// <param name="THCArcOn">Switches the THC arc on virtual signal on.</param>
    /// <param name="THCUp">Switches the THC up virtual signal on.</param>
    /// <param name="THCDown">Switches the THC down virtual signal on.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetTHCVirtualSignal([MarshalAs(UnmanagedType.I1)] bool THCArcOn, [MarshalAs(UnmanagedType.I1)] bool THCUp, [MarshalAs(UnmanagedType.I1)] bool THCDown);        //THC virtuális jelek beállítása

    /// <summary>
    /// Gets the THC virtual control signals
    /// </summary>
    /// <param name="THCArcOn">Switches the THC arc on virtual signal on.</param>
    /// <param name="THCUp">Switches the THC up virtual signal on.</param>
    /// <param name="THCDown">Switches the THC down virtual signal on.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetTHCVirtualSignal([MarshalAs(UnmanagedType.I1)] ref bool THCArcOn, [MarshalAs(UnmanagedType.I1)] ref bool THCUp, [MarshalAs(UnmanagedType.I1)] ref bool THCDown);     //THC virtuális jelek kiolvasása

    /// <summary>
    /// Gets the probed coordinates of a probing operation.
    /// </summary>
    /// <param name="Xpos">X coordinate.</param>
    /// <param name="Ypos">Y coordinate.</param>
    /// <param name="Zpos">Z coordinate.</param>
    /// <param name="Apos">A coordinate.</param>
    /// <param name="Bpos">B coordinate.</param>
    /// <param name="Cpos">C coordinate.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetProbePosition(ref double Xpos, ref double Ypos, ref double Zpos, ref double Apos, ref double Bpos, ref double Cpos);

    /// <summary>
    /// Switches the safe probe mode on.
    /// Stops the motion if this mode is on and if the probe input triggers while motion.
    /// </summary>
    /// <param name="Enable">on/off.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SafeProbeEnable([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// Switches the jogsafe probe mode on.
    /// Stops the jog movement and disables that jog direction if the probe is hit while jogging.
    /// </summary>
    /// <param name="Enable">on/off.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JogSafeProbeEnable([MarshalAs(UnmanagedType.I1)] bool Enable);

    /// <summary>
    /// Gets the probe input state.
    /// </summary>
    /// <param name="Finish">false=OK, true=probing finished.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetProbeState([MarshalAs(UnmanagedType.I1)] ref bool Finish);

    /// <summary>
    /// Gets the system statistics.
    /// </summary>
    /// <param name="_Statistics">Statistics structure with the statistics parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetStatistics(ref Statistics _Statistics);

    /// <summary>
    /// Sets the system statistics.
    /// </summary>
    /// <param name="_Statistics">Statistics structure with the statistics parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetStatistics(ref Statistics _Statistics);

    /// <summary>
    /// Gets the system communication latency. it returns a pointer with 500 data bytes. Each data bytes represents one latency point.
    /// </summary>
    /// <param name="ThreadUsage">Communication latency data byte array.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetThreadUsage(out IntPtr ThreadUsage);

    /// <summary>
    /// Reads the auxiliary encoder counts per turn.
    /// </summary>
    /// <param name="Num">Number of aux encoder (0 to 5).</param>
    /// <param name="CountsPer">Counts per turn value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAuxEncoderCountsPer(int Num, ref double CountsPer);

    /// <summary>
    /// Sets the auxiliary encoder counts per turn.
    /// </summary>
    /// <param name="Num">Number of aux encoder (0 to 5).</param>
    /// <param name="CountsPer">Counts per turn value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAuxEncoderCountsPer(int Num, double CountsPer);

    /// <summary>
    /// Sets the auxiliary encoder position.
    /// </summary>
    /// <param name="Num">Number of aux encoder (0 to 5).</param>
    /// <param name="Pos">Position of the encoder.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetAuxEncoderPos(int Num, double Pos);

    /// <summary>
    /// Gets the auxiliary encoder position.
    /// </summary>
    /// <param name="Num">Number of aux encoder (0 to 5).</param>
    /// <param name="Pos">Position of the encoder.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetAuxEncoderPos(int Num, ref double Pos);

    /// <summary>
    /// Sets the spindle closed loop PID controller parameters.
    /// </summary>
    /// <param name="_SpindlePID">Spindle PID parameters structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int SetSpindlePID(ref SpindlePID _SpindlePID);

    /// <summary>
    /// Gets the spindle closed loop PID controller parameters.
    /// </summary>
    /// <param name="_SpindlePID">Spindle PID parameters structure.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetSpindlePID(ref SpindlePID _SpindlePID);

    /// <summary>
    /// Gets the spindle PID feedback data in an array of 500.
    /// </summary>
    /// <param name="CommandedRPM">The programmed spindle RPM.</param>
    /// <param name="MeasuredRPM">The measured spindle RPM.</param>
    /// <param name="PIDOut">The calculated PID PWM value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetPIDData(out IntPtr CommandedRPM, out IntPtr MeasuredRPM, out IntPtr PIDOut);

    /// <summary>
    /// Gets the actual spindle PID PWM output.
    /// </summary>
    /// <param name="PidOut">The calculated PID PWM value.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetPIDOut(ref double PidOut);

    /// <summary>
    /// Tells the controller that motion is in progress. Set this function true before motion and set it false after all motion ended.
    /// Required for THC control operations only.
    /// </summary>
    /// <param name="Run">Motion in progress.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetMotionProgressState([MarshalAs(UnmanagedType.I1)] bool Run);

    /// <summary>
    /// Sets the state of 10 dedicated fast outputs.
    /// </summary>
    /// <param name="_OutPin">Outpin structure which holds the pin states.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int SetOutputPin(ref OutPin _OutPin);

    /// <summary>
    /// Gets the state of 10 dedicated fast outputs.
    /// </summary>
    /// <param name="_OutPin">Outpin structure which holds the pin states.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    unsafe public static extern int GetOutputPin(ref OutPin _OutPin);

    /// <summary>
    /// Sets the sate of the output pins to high level.
    /// </summary>
    /// <param name="Num">Integer number in which every bit represents one output pin.(0-63)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetOutputBit(int Num);

    /// <summary>
    /// Sets the sate of the output pins to low level.
    /// </summary>
    /// <param name="Num">Integer number in which every bit represents one output pin.(0-63)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ClearOutputBit(int Num);

    /// <summary>
    /// Selects the feedrate mode.
    /// Use the FeedMode enum.
    /// Note: G95 is not implemented, is to be implemented in future releases of the API.
    /// </summary>
    /// <param name="Num">Feedrate mode, FeedMode enum.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetFeedMode(int Num);

    /// <summary>
    /// Gets the feedrate mode.
    /// </summary>
    /// <param name="Num">Feedrate mode, FeedMode enum.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetFeedMode(ref int Num);                    //Feed üzemmód lekérdezése

    /// <summary>
    /// Sets the tangential cutter knife parameters and enables the tangential cutter option.
    /// </summary>
    /// <param name="_TangentCutterParam">Tangential cutter knife parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetTangentialCutter([MarshalAs(UnmanagedType.Struct)] ref TangentCutterParam _TangentCutterParam);

    /// <summary>
    /// Gets the tangential cutter knife parameters and enables the tangential cutter option.
    /// </summary>
    /// <param name="_TangentCutterParam">Tangential cutter knife parameters.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    [DllImport("UC100.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetTangentialCutter([MarshalAs(UnmanagedType.Struct)] ref TangentCutterParam _TangentCutterParam);   //Tangenciális vágás paramétereinek lekérdezése

    /// <summary>
    /// Loads the laser picture image data array to memory.
    /// </summary>
    /// <param name="_LaserPictureData">The laser picture data structure.</param>
    /// <param name="BrightnessDataInput">The laser picture brightness byte array.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    public static int SetPictureData(LaserPictureData _LaserPictureData, byte[][] BrightnessDataInput)
    {
        int counter = 0;
        byte[] BrightnessData = new byte[BrightnessDataInput.Length * BrightnessDataInput[0].Length];
        for (int i = 0; i < BrightnessDataInput.Length; i++)
        {
            for (int j = 0; j < BrightnessDataInput[i].Length; j++)
            {
                BrightnessData[counter] = BrightnessDataInput[i][j];
                counter++;
            }
        }
        unsafe
        {
            fixed (byte* Data = BrightnessData)
            {
                _LaserPictureData.BrightnessData = Data;
                return UC100.SetLaserPictureData(ref _LaserPictureData);
            }
        }
    }

    /// <summary>
    /// Adds a spindle syncronised motion (Thread cutting.) to the motion buffer for execution.
    /// This command is the same as the AddYncMove, but parameters can be null for no movement.
    /// </summary>
    /// <param name="x">Movement endpoint on the X axis.</param>
    /// <param name="z">Movement endpoint on the Z axis.</param>
    /// <param name="Pitch">Thread pitch.</param>
    /// <param name="Degree">The degree to plunge in.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    public static int AddSyncMoveP(double? x, double? z, double Pitch, double? Degree, int ID)
    {
        double xp, zp, de;
        if (x != null)
            xp = (double)x;
        else
            xp = double.MaxValue;
        if (z != null)
            zp = (double)z;
        else
            zp = double.MaxValue;
        if (Degree != null)
            de = (double)Degree;
        else
            de = double.MaxValue;
        return (AddSyncMove(xp, zp, Pitch, de, ID));
    }

    /// <summary>
    /// Adds one circular movement to the motion buffer for execution. Plane 0=XY, 1=XZ, 2=YZ. NextID is the ID of the next command for proper stop and restart on the end of the arc.
    /// This command is the same as the AddCircularMove, but parameters can be null for no movement.
    /// </summary>
    /// <param name="x">X axis new position in Units.</param>
    /// <param name="y">Y axis new position in Units.</param>
    /// <param name="z">Z axis new position in Units.</param>
    /// <param name="rx">Arc center X coordinate in Units.</param>
    /// <param name="ry">Arc center Y coordinate in Units.</param>
    /// <param name="rz">Arc center Z coordinate in Units.</param>
    /// <param name="Dir">Arc direction (CW/CCW)</param>
    /// <param name="Feed">Path feedrate in Units/second.</param>
    /// <param name="Plane">Arc plane (0=XY, 1=XZ, 2=YZ)</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <param name="NextID">Identifier of the next command.(Required for proper stop and restart at the end of the arc.)</param>
    /// <returns>ReturnVal, UC error code.</returns>
    public static int AddCircularMoveP(double? x, double? y, double? z, double rx, double ry, double rz, bool Dir, double Feed, int Plane, int ID, int NextID)
    {
        double xp, yp, zp;
        if (x != null)
            xp = (double)x;
        else
            xp = double.MaxValue;
        if (y != null)
            yp = (double)y;
        else
            yp = double.MaxValue;
        if (z != null)
            zp = (double)z;
        else
            zp = double.MaxValue;

        return (AddCircularMove(xp, yp, zp, rx, ry, rz, Dir, Feed, Plane, ID, NextID));
    }

    /// <summary>
    /// Adds one linear movement to the motion buffer for execution.
    /// This command is the same as the AddLinearMove, but parameters can be null for no movement.
    /// </summary>
    /// <param name="x">X axis new position in Units.</param>
    /// <param name="y">Y axis new position in Units.</param>
    /// <param name="z">Z axis new position in Units.</param>
    /// <param name="a">A axis new position in Units.</param>
    /// <param name="b">B axis new position in Units.</param>
    /// <param name="c">C axis new position in Units.</param>
    /// <param name="Feed">Path feedrate in Units/second.</param>
    /// <param name="ID">Identifier of the motion command.</param>
    /// <returns>ReturnVal, UC error code.</returns>
    public static int AddLinearMoveP(double? x, double? y, double? z, double? a, double? b, double? c, double Feed, int ID)
    {
        double xp, yp, zp, ap, bp, cp;
        if (x != null)
            xp = (double)x;
        else
            xp = double.MaxValue;
        if (y != null)
            yp = (double)y;
        else
            yp = double.MaxValue;
        if (z != null)
            zp = (double)z;
        else
            zp = double.MaxValue;
        if (a != null)
            ap = (double)a;
        else
            ap = double.MaxValue;
        if (b != null)
            bp = (double)b;
        else
            bp = double.MaxValue;
        if (c != null)
            cp = (double)c;
        else
            cp = double.MaxValue;
        return AddLinearMove(xp, yp, zp, ap, bp, cp, Feed, ID);
    }

    /// <summary>
    /// Encoder settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct EncoderSetting
    {
        /// <summary>
        /// Enables the MPG.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MPGEnable;
        /// <summary>
        /// MPG encoder A channel input pin.
        /// </summary>
        public int MPGPinA;
        /// <summary>
        /// MPG encoder B channel input pin.
        /// </summary>
        public int MPGPinB;
        /// <summary>
        /// MPG prescaler 0-100 integer.
        /// </summary>
        public int MPGPrescale;
        /// <summary>
        /// MPG filter constant 0-100 integer.
        /// </summary>
        public int MPGFilter;
        /// <summary>
        /// MPG speed multiplier 0-1000 integer.
        /// </summary>
        public double MPGSpeedMultiplier;
        /// <summary>
        /// Attach JRO function for the MPG.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EnableJROToMPG;
        /// <summary>
        /// Spindle encoder A channel input pin.
        /// </summary>
        public int EncoderPinA;
        /// <summary>
        /// Spindle encoder B channel input pin.
        /// </summary>
        public int EncoderPinB;
        /// <summary>
        /// Inverts the encoder count direction.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EncoderReverseDirection;
        /// <summary>
        /// Encoder counts per spindle rotation.
        /// </summary>
        public int EncoderPPR;
        /// <summary>
        /// Encoder to spindle gear ratio. Value=1 if there is no gearing.
        /// </summary>
        public double EncoderCorrection;
        /// <summary>
        /// 1. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux1EncoderPinA;
        /// <summary>
        /// 1. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux1EncoderPinB;
        /// <summary>
        /// 2. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux2EncoderPinA;
        /// <summary>
        /// 2. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux2EncoderPinB;
        /// <summary>
        /// 3. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux3EncoderPinA;
        /// <summary>
        /// 3. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux3EncoderPinB;
        /// <summary>
        /// 4. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux4EncoderPinA;
        /// <summary>
        /// 4. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux4EncoderPinB;
        /// <summary>
        /// 5. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux5EncoderPinA;
        /// <summary>
        /// 5. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux5EncoderPinB;
        /// <summary>
        /// 6. auxiliary encoder A channel input pin.
        /// </summary>
        public int Aux6EncoderPinA;
        /// <summary>
        /// 6. auxiliary encoder B channel input pin.
        /// </summary>
        public int Aux6EncoderPinB;
        /// <summary>
        /// 1. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux1EncoderStepPer;
        /// <summary>
        /// 2. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux2EncoderStepPer;
        /// <summary>
        /// 3. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux3EncoderStepPer;
        /// <summary>
        /// 4. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux4EncoderStepPer;
        /// <summary>
        /// 5. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux5EncoderStepPer;
        /// <summary>
        /// 6. auxiliary encoder steps per unit value.
        /// </summary>
        public double Aux6EncoderStepPer;
    }

    /// <summary>
    /// MPG settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MPGSetting
    {
        /// <summary>
        /// Enables the 1. MPG.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MPG1Enable;
        /// <summary>
        /// Enables the 2. MPG.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MPG2Enable;
        /// <summary>
        /// 1. MPG channel A input.
        /// </summary>
        public int MPG1PinA;
        /// <summary>
        /// 1. MPG channel B input.
        /// </summary>
        public int MPG1PinB;
        /// <summary>
        /// 2. MPG channel A input.
        /// </summary>
        public int MPG2PinA;
        /// <summary>
        /// 2. MPG channel B input.
        /// </summary>
        public int MPG2PinB;
        /// <summary>
        /// 1. MPG prescaler 0-100 integer value.
        /// </summary>
        public int MPG1Prescale;
        /// <summary>
        /// 2. MPG prescaler 0-100 integer value.
        /// </summary>
        public int MPG2Prescale;
        /// <summary>
        /// 1. MPG filer constant 0-100 integer value.
        /// </summary>
        public int MPG1Filter;
        /// <summary>
        /// 2. MPG filer constant 0-100 integer value.
        /// </summary>
        public int MPG2Filter;
        /// <summary>
        /// 1. MPG speed multiplier 0-1000 integer value.
        /// </summary>
        public double MPG1SpeedMultiplier;
        /// <summary>
        /// 2. MPG speed multiplier 0-1000 integer value.
        /// </summary>
        public double MPG2SpeedMultiplier;
        /// <summary>
        /// Enables the JRO for the MPG.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EnableJROToMPG;
    }

    /// <summary>
    /// Axis settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AxisSetting
    {
        /// <summary>
        /// Axis number (X=0,Y=1,Z=2,A=3,B=4,C=5).
        /// </summary>
        public int Axis;
        /// <summary>
        /// Enables the axis.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Enable;
        /// <summary>
        /// Step output pin.
        /// </summary>
        public int StepPin;
        /// <summary>
        /// Direction output pin.
        /// </summary>
        public int DirPin;
        /// <summary>
        /// Inverts the step pin.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool StepNeg;
        /// <summary>
        /// Inverts the dir pin.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool DirNeg;
        /// <summary>
        /// Acceleration parameter of the axis in Units/sec^2.
        /// </summary>
        public double MaxAccel;
        /// <summary>
        /// Velocity parameter of the axis in Units/sec.
        /// </summary>
        public double MaxVel;
        /// <summary>
        /// Steps per Units parameter.
        /// </summary>
        public double StepPer;
        /// <summary>
        /// Home input pin of the axis.
        /// </summary>
        public int HomePin;
        /// <summary>
        /// Inverts the home input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool HomeNeg;
        /// <summary>
        /// Positive side end-limit input pin.
        /// </summary>
        public int LimitPPin;
        /// <summary>
        /// Inverts the positive side end-limit input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool LimitPNeg;
        /// <summary>
        /// Negative side end-limit input pin.
        /// </summary>
        public int LimitNPin;
        /// <summary>
        /// Inverts the negative side end-limit input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool LimitNNeg;
        /// <summary>
        /// Positive side software end-limit.
        /// </summary>
        public double SoftLimitP;
        /// <summary>
        /// Negative side software end-limit.
        /// </summary>
        public double SoftLimitN;
        /// <summary>
        /// Slave axis, only for the XYZ axes. (0=No slave, 3=A slave, 4=B slave, 5=C slave).
        /// </summary>
        public int SlaveAxis;
        /// <summary>
        /// Enables the backlash compensation for the axis.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool BacklashOn;
        /// <summary>
        /// Backlash distance in Units.
        /// </summary>
        public double BacklashDist;
        /// <summary>
        /// Compensation acceleration for backlash and thread cutting in Units/sec^2.
        /// </summary>
        public double CompAccel;
        /// <summary>
        /// Axis enable output pin.
        /// </summary>
        public int EnablePin;
        /// <summary>
        /// Inverts the axis enable output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EnablePinNeg;
        /// <summary>
        /// Delays the enable output 0-255 value (x10msec).
        /// </summary>
        public int EnableDelay;
        /// <summary>
        /// Current hi/low output pin.
        /// </summary>
        public int CurrentHiLowPin;
        /// <summary>
        /// Inverts the current hi/low output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool CurrentHiLowPinNeg;
        /// <summary>
        /// Home back off distance in Units.
        /// </summary>
        public double HomeBackOff;
        /// <summary>
        /// Enables the rotary axis function for the axis. Works for the A,B,C axes only.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool RotaryAxis;
        /// <summary>
        /// Enables the rollover for rotary axis on 360 degrees. Works for the A,B,C axes only and if the rotary axis function is enabled.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool RotaryRollover;
    }

    /// <summary>
    /// Spindle settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SPSetting
    {
        /// <summary>
        /// Spindle mode (0=Not used, 1=PWM spindle mode, 2=Step/dir spindle mode).
        /// </summary>
        public int Mode;
        /// <summary>
        /// Spindle PWM or step output pin.
        /// </summary>
        public int SPPin;
        /// <summary>
        /// Inverts the spindle PWM output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SPPinNeg;
        /// <summary>
        /// Spindle direction output pin.
        /// </summary>
        public int SPDirPin;
        /// <summary>
        /// Inverts the spindle direction output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SPDirPinNeg;
        /// <summary>
        /// Spindle PWM frequency. (1 to 5000 Hz, resolution is 100000/value.)
        /// </summary>
        public int PWMFreq;
        /// <summary>
        /// Spindle acceleration for step/dir spindle in rev/second^2.
        /// </summary>
        public double SPAccel;
        /// <summary>
        /// Steps per spindle turn value for step/dir spindle.
        /// </summary>
        public double SPStepPerRev;
        /// <summary>
        /// Minimum spindle velocity (1/minute).
        /// </summary>
        public double SPMinVel;
        /// <summary>
        /// Maximum spindle velocity (1/minute).
        /// </summary>
        public double SPMaxVel;
        /// <summary>
        /// Minimum PWM duty cycle percentage for PWM spindle.
        /// </summary>
        public double SPMinPWMPercent;
        /// <summary>
        /// Maximum PWM duty cycle percentage for PWM spindle.
        /// </summary>
        public double SPMaxPWMPercent;
        /// <summary>
        /// Spindle relay M3 output pin.
        /// </summary>
        public int SPM3Pin;
        /// <summary>
        /// Inverts the spindle relay M3 output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SPM3PinNeg;
        /// <summary>
        /// Spindle relay M4 output pin.
        /// </summary>
        public int SPM4Pin;
        /// <summary>
        /// Inverts the spindle relay M4 output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SPM4PinNeg;
        /// <summary>
        /// Spindle relays M3 and M4 enable.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EnableM3M4Output;
    }

    /// <summary>
    /// THC settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct THCSetting
    {
        /// <summary>
        /// THC on input pin.
        /// </summary>
        public int THCOnPin;
        /// <summary>
        /// Inverts the THC on input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCOnPinNeg;
        /// <summary>
        /// THC up input pin.
        /// </summary>
        public int THCUpPin;
        /// <summary>
        /// Inverts the THC up input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCUpPinNeg;
        /// <summary>
        /// THC down input pin.
        /// </summary>
        public int THCDownPin;
        /// <summary>
        /// Inverts the THC down input.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCDownPinNeg;
        /// <summary>
        /// Z axis minimum height position for THC control.
        /// </summary>
        public double THCmin;
        /// <summary>
        /// Z axis maximum height position for THC control.
        /// </summary>
        public double THCmax;
        /// <summary>
        /// THC control feedrate in Units per seconds.
        /// </summary>
        public double THCFeed;
        /// <summary>
        /// THC control always on.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAlwaysOn;
        /// <summary>
        /// THC on delay time in seconds. (0-10sec range)
        /// </summary>
        public double THCOnTime;
        /// <summary>
        /// THC antidive velocity percentage trigger level. (0-100%)
        /// </summary>
        public double THCAntiDiveVel;
        /// <summary>
        /// THC enable output pin.
        /// </summary>
        public int THCEnPin;
        /// <summary>
        /// Inverts the THC enable output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCEnPinNeg;
        /// <summary>
        /// THC antidive output pin.
        /// </summary>
        public int THCAntiDivePin;
        /// <summary>
        /// Inverts the THC antidive output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAntiDivePinNeg;
        /// <summary>
        /// THC anti dive output pin.
        /// </summary>
        public int THCAntiDownPin;
        /// <summary>
        /// Inverts the THC anti down output.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAntiDownPinNeg;

    }

    /// <summary>
    /// Status structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Stat
    {
        /// <summary>
        /// True when the motion controller is in idle.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Idle;
        /// <summary>
        /// True when jog command is being executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Jog;
        /// <summary>
        /// True when dwell command is being executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Dwell;
        /// <summary>
        /// True when backlash compensation is being executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Backlash;
        /// <summary>
        /// True when homing command is being executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Home;
        /// <summary>
        /// True when probing command is being executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Probe;
        /// <summary>
        /// True when the e-stop input is active.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Estop;
        /// <summary>
        /// True when the motion controller is in software limits.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SoftLimit;
        /// <summary>
        /// True when the hardware limit input is active.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool HardLimit;
        /// <summary>
        /// The data size in the motion buffer.
        /// </summary>
        public int Puffer;
        /// <summary>
        /// The actual feedrate of the motion in Units per seconds.
        /// </summary>
        public double Feed;
        /// <summary>
        /// The actual spindle RPM measured with the spindle index or with the spindle encoder.
        /// </summary>
        public double SpindleRPM;
        /// <summary>
        /// The identifier (ID) of the current motion command.
        /// </summary>
        public int CurrentID;
        /// <summary>
        /// True if the limit override is on.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool LimitOverride;
        /// <summary>
        /// The programmed feedrate of the current motion command.
        /// </summary>
        public double OriginalFeed;
        /// <summary>
        /// True when there is a jog command on the MPG 1.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MPG1JogOn;
        /// <summary>
        /// True when there is a jog command on the MPG 2.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MPG2JogOn;
        /// <summary>
        /// True when the THC on delay is ongoing.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCOnWaiting;
        /// <summary>
        /// True when spindle syncronous motion is ongoing.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SyncThread;
        /// <summary>
        /// True when the spindle is switched on.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SpindleOn;
        /// <summary>
        /// Shows the spindle rotation direction.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SpindleDir;
        /// <summary>
        /// True when laser engraving is ongoing.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool LaserRunning;
        /// <summary>
        /// True when laser data is loaded.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool LaserDataValid;
        /// <summary>
        /// True when the THC control is enabled.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCEnabled;
        /// <summary>
        /// True when the THC anti dive is active.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAntiDive;
        /// <summary>
        /// True when the THC anti dive function is enabled.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAntiDiveEnable;
        /// <summary>
        /// True when the THC delay function is enabled.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCDelayEnable;
        /// <summary>
        /// True when the THC anti down function is enabled.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool THCAntiDownEnable;
        /// <summary>
        /// True when the probe input is active. The probejogmask applies.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool ProbeActive;
    }

    /// <summary>
    /// Hardware/software version structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Versio
    {
        /// <summary>
        /// Motion controller firmware version number.
        /// </summary>
        public int FWVersion;
        /// <summary>
        /// Motion controller hardware version number.
        /// </summary>
        public int HWVersion;
        /// <summary>
        /// API dll version number.
        /// </summary>
        public int DLLVersion;
        /// <summary>
        /// Motion controller serial number in decimal value. (Should be converted to hexadecimal.)
        /// </summary>
        public int SerialNumber;
    }

    /// <summary>
    /// Trajectory parameters structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TrajParam
    {
        /// <summary>
        /// Below this vectors connection angle in degrees the controller executes the command without an exact stop.
        /// </summary>
        public double StopAngle;
        /// <summary>
        /// Constant velocity mode is used if true and exact stop mode is used if false.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool ConstantVel;
        /// <summary>
        /// This paramter is currently not implemented.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Units;
        /// <summary>
        /// The maximum path deviation in Units. If 0 then all vectors are unified.
        /// </summary>
        public double LinearErr;
        /// <summary>
        /// The maximum total length in Units of the unified vectors.
        /// </summary>
        public double MaxLinearUnificationLenght;
        /// <summary>
        /// The maximum length of the vectors to be unified.
        /// </summary>
        public double MaxLinearAddLenght;
        /// <summary>
        /// Maximum path error on corners.
        /// </summary>
        public double CornerError;
        /// <summary>
        /// The communication buffer length in seconds (0.05 to 1 sec)
        /// </summary>
        public double USBPufferSize;
    }

    /// <summary>
    /// Spindle PID controller structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SpindlePID
    {
        /// <summary>
        /// Enables the spindle PID controller.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EnablePID;
        /// <summary>
        /// PID loop time constant (10mseconds divided with 1 to 255).
        /// </summary>
        public int PIDTimePrescaler;
        /// <summary>
        /// Proportional gain of the spindle PID controller.
        /// </summary>
        public double Kp;
        /// <summary>
        /// Integral gain of the spindle PID controller.
        /// </summary>
        public double Ki;
        /// <summary>
        /// Derivative gain of the spindle PID controller.
        /// </summary>
        public double Kd;
    }

    /// <summary>
    /// Feedrate mode enumerator.
    /// </summary>
    public enum FeedMode
    {
        /// <summary>
        /// Inverse time feedrate mode
        /// </summary>
        G93 = 0,
        /// <summary>
        /// Units per minute feedrate mode.
        /// </summary>
        G94 = 1,
        /// <summary>
        /// Units per spindle revolution feedrate mode.
        /// Note: This mode is not implemented yet.
        /// </summary>
        G95 = 2
    }

    /// <summary>
    /// UC return value, error codes enumerator.
    /// </summary>
    public enum ReturnVal
    {
        /// <summary>
        /// Command executed without error.
        /// </summary>
        UC100_OK = 0,
        /// <summary>
        /// Error 1.: Device was not opened yet.
        /// </summary>
        UC100_DEVICE_NOT_OPENED = 1,
        /// <summary>
        /// Error 2.: Device not found.
        /// </summary>
        UC100_DEVICE_NOT_FOUND = 2,
        /// <summary>
        /// Error 3.: Firmware error.
        /// </summary>
        UC100_FIRMWARE_ERROR = 3,
        /// <summary>
        /// Error 4.: Communication error.
        /// </summary>
        UC100_IO_ERROR = 4,
        /// <summary>
        /// Error 5.: Motion in progress. Can be cleared with the Stop() function.
        /// </summary>
        UC100_MOVEMENT_IN_PROGRESS = 5,
        /// <summary>
        /// Error 6.: Homing in progress. Can be cleared with the Stop() function.
        /// </summary>
        UC100_HOME_IN_PROGRESS = 6,
        /// <summary>
        /// Error 7.: Controller is in estop state. Can be cleared with the Stop() function.
        /// </summary>
        UC100_ESTOP = 7,
        /// <summary>
        /// Error 8.: Limit input(s) active. Can be cleared with the LimitOverRide() function.
        /// </summary>
        UC100_LIMIT = 8,
        /// <summary>
        /// Error 9.: Function parameter error. The function was called with errorous parameter(s).
        /// </summary>
        UC100_PARAMETER_ERROR = 9,
        /// <summary>
        /// Error 10.: Function buffer is full.
        /// </summary>
        UC100_COMMAND_BUFFER_FULL = 10,
        /// <summary>
        /// Error 10.: Firmware update is in progress.
        /// </summary>
        UC100_FW_UPDATE = 11,
        /// <summary>
        /// Error 11.: Probe input is active.
        /// </summary>
        UC100_PROBEPIN_ACTIVE = 12
    };

    /// <summary>
    /// Device type enumerator.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// Demo AXBB controller.
        /// </summary>
        Demo_AXBB = -19,
        /// <summary>
        /// Demo UC300ETH-UB1 motherboard.
        /// </summary>
        Demo_UC300ETH_UB1 = -18,
        /// <summary>
        /// Demo UC300ETH-M45 motherboard.
        /// </summary>
        Demo_UC300ETH_M45 = -17,
        /// <summary>
        /// Demo UC300ETH-5441 motherboard.
        /// </summary>
        Demo_UC300ETH_5441 = -16,
        /// <summary>
        /// Demo UC300-5441 motherboard.
        /// </summary>
        Demo_UC300_5441 = -15,
        /// <summary>
        /// Demo UC300ETH-STEPPER motherboard.
        /// Note: Not implemented.
        /// </summary>
        Demo_UC300ETH_STEPPER = -14,
        /// <summary>
        /// Demo UC300ETH-ISOBOB motherboard.
        /// Note: Not implemented.
        /// </summary>
        Demo_UC300ETH_ISOBOB = -13,
        /// <summary>
        /// Demo UC300ETH-MK44 motherboard.
        /// </summary>
        Demo_UC300ETH_M44 = -12,
        /// <summary>
        /// Demo UC300ETH-5LPT motherboard.
        /// </summary>
        Demo_UC300ETH_5LPT = -11,
        /// <summary>
        /// Demo UC300ETH module with outputs low level.
        /// </summary>
        Demo_UC300ETH_Low = -10,
        /// <summary>
        /// Demo UC300ETH module with outputs high level.
        /// </summary>
        Demo_UC300ETH_Hi = -9,
        /// <summary>
        /// Demo UC400ETH controller.
        /// </summary>
        Demo_UC400ETH = -8,
        /// <summary>
        /// Demo UC300-STEPPER motherboard.
        /// Note: Not implemented.
        /// </summary>
        Demo_UC300_STEPPER = -7,
        /// <summary>
        /// Demo UC300-ISOBOB motherboard.
        /// Note: Not implemented.
        /// </summary>
        Demo_UC300_ISOBOB = -6,
        /// <summary>
        /// Demo UC300-MK44 motherboard.
        /// </summary>
        Demo_UC300_M44 = -5,
        /// <summary>
        /// Demo UC300-5LPT motherboard.
        /// </summary>
        Demo_UC300_5LPT = -4,
        /// <summary>
        /// Demo UC300 module with outputs low level.
        /// </summary>
        Demo_UC300_Low = -3,
        /// <summary>
        /// Demo UC300 module with outputs high level.
        /// </summary>
        Demo_UC300_Hi = -2,
        /// <summary>
        /// Demo UC100 controller.
        /// </summary>
        Demo_UC100 = -1,
        /// <summary>
        /// Demo UC100 controller.
        /// </summary>
        Demo_mode = 0,
        /// <summary>
        /// UC100 controller.
        /// </summary>
        UC100 = 1,
        /// <summary>
        /// UC300 module with outputs high level.
        /// </summary>
        UC300_Hi = 2,
        /// <summary>
        /// UC300 module with outputs low level.
        /// </summary>
        UC300_Low = 3,
        /// <summary>
        /// UC300-5LPT motherboard.
        /// </summary>
        UC300_5LPT = 4,
        /// <summary>
        /// UC300-MK44 motherboard.
        /// </summary>
        UC300_M44 = 5,
        /// <summary>
        /// UC300-ISOBOB motherboard.
        /// Note: Not implemented.
        /// </summary>
        UC300_ISOBOB = 6,
        /// <summary>
        /// UC300-STEPPER motherboard.
        /// Note: Not implemented.
        /// </summary>
        UC300_STEPPER = 7,
        /// <summary>
        /// UC400ETH motherboard.
        /// </summary>
        UC400ETH = 8,
        /// <summary>
        /// UC300ETH module with outputs high level.
        /// </summary>
        UC300ETH_Hi = 9,
        /// <summary>
        /// UC300ETH module with outputs low level.
        /// </summary>
        UC300ETH_Low = 10,
        /// <summary>
        /// UC300ETH-5LPT motherboard.
        /// </summary>
        UC300ETH_5LPT = 11,
        /// <summary>
        /// UC300ETH-MK44 motherboard.
        /// </summary>
        UC300ETH_M44 = 12,
        /// <summary>
        /// UC300ETH-ISOBOB motherboard.
        /// Note: Not implemented.
        /// </summary>
        UC300ETH_ISOBOB = 13,
        /// <summary>
        /// UC300ETH-STEPPER motherboard.
        /// Note: Not implemented.
        /// </summary>
        UC300ETH_STEPPER = 14,
        /// <summary>
        /// UC300-5441 motherboard.
        /// </summary>
        UC300_5441 = 15,
        /// <summary>
        /// UC300ETH-5441 motherboard.
        /// </summary>
        UC300ETH_5441 = 16,
        /// <summary>
        /// UC300ETH-M45 motherboard.
        /// </summary>
        UC300ETH_M45 = 17,
        /// <summary>
        /// UC300ETH-UB1 motherboard.
        /// </summary>
        UC300ETH_UB1 = 18,
        /// <summary>
        /// AXBB controller.
        /// </summary>
        AXBB = 19
    };

    /// <summary>
    /// Analog settings structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AnalogSetting
    {
        /// <summary>
        /// Attaches the feedrate override (FRO) to analog input channel.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FROAnalogEnable;
        /// <summary>
        /// FRO analog input channel number (0=no input, 1=channel 1., 2=channel 2., 3=channel 3., 4= channel 4.).
        /// </summary>
        public int FROAnalogIn;
        /// <summary>
        /// FRO minimum percentage (0-300 range).
        /// </summary>
        public double FROMinPercent;
        /// <summary>
        /// FRO maximum percentage (0-300 range).
        /// </summary>
        public double FROMaxPercent;
        /// <summary>
        /// Attaches the spindle rate override (SRO) to analog input channel.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SROAnalogEnable;
        /// <summary>
        /// SRO analog input channel number (0=no input, 1=channel 1., 2=channel 2., 3=channel 3., 4=channel 4.).
        /// </summary>
        public int SROAnalogIn;
        /// <summary>
        /// SRO minimum percentage (0-300 range).
        /// </summary>
        public double SROMinPercent;
        /// <summary>
        /// SRO maximum percentage (0-300 range).
        /// </summary>
        public double SROMaxPercent;
        /// <summary>
        /// Enables the spindle PWM proportional voltage output on the analog output channel.
        /// (0=no output, 1=channel 1., 2=channel 2., 3=channel 3., 4= channel 4.).
        /// </summary>
        public int SpindlePWMEnable;
        /// <summary>
        /// Attaches the jog rate override (JRO) to analog input channel.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool JROAnalogEnable;
        /// <summary>
        /// JRO analog input channel number (0=no input, 1=channel 1., 2=channel 2., 3=channel 3., 4=channel 4.).
        /// </summary>
        public int JROAnalogIn;
        /// <summary>
        /// JRO minimum percentage (0-100 range).
        /// </summary>
        public double JROMinPercent;
        /// <summary>
        /// JRO maximum percentage (0-100 range).
        /// </summary>
        public double JROMaxPercent;
    };

    /// <summary>
    /// Laser picture data structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct LaserPictureData
    {
        /// <summary>
        /// Laser mode (0=Grayscale with PWM output, 1=Halftone, 2=Black and white).
        /// </summary>
        public int LaserMode;
        /// <summary>
        /// X start position for the laser picture engraving.
        /// </summary>
        public double StartPosX;
        /// <summary>
        /// Y start position for the laser picture engraving.
        /// </summary>
        public double StartPosY;
        /// <summary>
        /// Laser picture engraving feedrate in Units/sec.
        /// </summary>
        public double Feed;
        /// <summary>
        /// Point X dimension in Units.
        /// </summary>
        public double PointXsize;
        /// <summary>
        /// Point Y dimension in Units.
        /// </summary>
        public double PointYsize;
        /// <summary>
        /// Picture points brightness data in a byte array pointer.
        /// </summary>
        public byte* BrightnessData;
        /// <summary>
        /// Picture array X dimension (number of points in X).
        /// </summary>
        public int ArrayXsize;
        /// <summary>
        /// Picture array Y dimension (number of points in Y).
        /// </summary>
        public int ArrayYsize;
    };

    /// <summary>
    /// Statistics structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct Statistics
    {
        /// <summary>
        /// Total distance run by the X axis.
        /// </summary>
        public double DistX;
        /// <summary>
        /// Total distance run by the X axis with running spindle.
        /// </summary>
        public double DistCutX;
        /// <summary>
        /// Total distance run by the Y axis.
        /// </summary>
        public double DistY;
        /// <summary>
        /// Total distance run by the Y axis with running spindle.
        /// </summary>
        public double DistCutY;
        /// <summary>
        /// Total distance run by the Z axis.
        /// </summary>
        public double DistZ;
        /// <summary>
        /// Total distance run by the Z axis with running spindle.
        /// </summary>
        public double DistCutZ;
        /// <summary>
        /// Total distance run by the A axis.
        /// </summary>
        public double DistA;
        /// <summary>
        /// Total distance run by the A axis with running spindle.
        /// </summary>
        public double DistCutA;
        /// <summary>
        /// Total distance run by the B axis.
        /// </summary>
        public double DistB;
        /// <summary>
        /// Total distance run by the B axis with running spindle.
        /// </summary>
        public double DistCutB;
        /// <summary>
        /// Total distance run by the C axis.
        /// </summary>
        public double DistC;
        /// <summary>
        /// Total distance run by the C axis with running spindle.
        /// </summary>
        public double DistCutC;
    };

    /// <summary>
    /// Fast output pin switch structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct OutPin
    {
        public int Out1Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out1PinNeg;
        public int Out2Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out2PinNeg;
        public int Out3Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out3PinNeg;
        public int Out4Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out4PinNeg;
        public int Out5Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out5PinNeg;
        public int Out6Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out6PinNeg;
        public int Out7Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out7PinNeg;
        public int Out8Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out8PinNeg;
        public int Out9Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out9PinNeg;
        public int Out10Pin;
        [MarshalAs(UnmanagedType.I1)]
        public bool Out10PinNeg;
    };

    /// <summary>
    /// Tangential cutter knife structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct TangentCutterParam
    {
        /// <summary>
        /// Enables the tangential cutter knife function.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool Enable;
        /// <summary>
        /// Knife stops above this connection angle.
        /// </summary>
        public double StopAtAngle;
        /// <summary>
        /// Knife retracts above this connection angle.
        /// </summary>
        public double RetractAtAngle;
        /// <summary>
        /// Knife retracts with this distance.
        /// </summary>
        public double Retract;
        /// <summary>
        /// Knife feedrate when put back to the workpiece after a retraction.
        /// </summary>
        public double MoveBackFeedrate;
        /// <summary>
        /// ???
        /// </summary>
        public double PullOutFeedrate;
    };

}


