/**
 *  MotorHelper UC100 wrapper class
 *  For use with Chopper Machines and IV-SV33MX Vision Systems
 *                      
 *  For Chopper:
 *                  - STEP 6
 *                  - DIR 7
 *                  - BLADE 8 (foot pedal must be down)
 *  
 *  Copyright: Z-Axis Connector Company
 *  Date:      12/22/23
 *  Author:    John Glatts
 */
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static UC100;

class MotorHelper
{
    private int boardID;
    private int stepXPin;
    private int dirXPin;
    private int enaXPin;
    private int xStepsPerUnit;
    public double speed;
    public double feedRate;
    public double cameraOffset;
    private int bladePin;
    public String helpStr;
    public bool useCamera;
    private bool isConnected;
    private double cutLength;
    private int cutQuantity;
    private CancellationTokenSource cancelTokenSource;
    private CancellationToken token;
    private string serialMsg;
    private double visionOffset;
    public double txtMPP;

    public MotorHelper()
    {
        setMotorVars();
        setHelpStr();
    }

    private void setMotorVars()
    {
        boardID = 1;
        speed = 8.5;
        feedRate = 0.85;
        stepXPin = 6;
        dirXPin = 7;
        enaXPin = 9;
        bladePin = 8;
        xStepsPerUnit = 8000;
        cameraOffset = 0.0;
        txtMPP = 1.0;
        useCamera = false;
        isConnected = false;
    }

    private void setHelpStr()
    {
        helpStr = "ERROR WITH DEVICE!\nIS DEVICE CONNECTED?\n\nFOLLOW STEPS BELOW\n";
        helpStr += "1. Make sure the UC100 is connected to the PC\n2. Hit 'Open Device'";
    }

    public bool enableMotor()
    {
        return setMotorAxis();
    }

    private bool getPinState(int pin)
    {
        int[] pinMap = { 10, 11, 12, 13, 15 };
        int bitPin = 0;
        long input = 0L;

        if (pin > 15 || pin < 10)
        {
            return false;
        }

        for (int i = 0; i < pinMap.Length; i++)
        {
            if (pin == pinMap[i])
            {
                bitPin = i;
                break;
            }
        }

        GetInput(ref input);
        return (input & (1 << bitPin)) != 0;
    }

    public void checkAllInputPins()
    {
        String s = "";
        for (int i = 10; i <= 15; i++)
        {
            s += "Pin #" + i + " state= " + getPinState(i).ToString() + "\n";
        }
        MessageBox.Show(s, "Z-Axis Connector Company");
    }

    public bool bladeUp()
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr);
            return false;
        }

        if (SetOutputBit(bladePin) != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
    }

    public bool chopOne()
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        bladeDown();
        // blade up settings
        Thread.Sleep(100);
        bladeUp();
        Thread.Sleep(100);

        // go back to pos. 
        /*
        if (Math.Abs(cameraOffset) > 0)
        {
            doMotorMove(cameraOffset, false);
            while (isMotorRunning()) ;
        }
        */

        return true;
    }

    public bool bladeDown()
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        if (ClearOutputBit(bladePin) != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
    }

    public bool setDevice()
    {
        bool ret = true;

        ret = listDevices();
        if (!ret)
            return ret;

        ret = getDeviceInfo();
        if (!ret)
            return ret;

        ret = deviceOpen();
        if (!ret)
            return ret;

        isConnected = true;
        setMotorAxis();
        bladeUp();

        return ret;
    }

    private bool setMotorAxis()
    {
        int ret = 0;

        AxisSetting xAxisSetting = getAxisSetup(0, stepXPin, dirXPin, enaXPin, 0, xStepsPerUnit, true, 10.0, 1000.0);
        ret = SetAxisSetting(ref xAxisSetting);

        if (ret != (int)ReturnVal.UC100_OK)
        {
            return false;
        }

        return true;
    }

    private AxisSetting getAxisSetup(int axis, int step, int dir, int enable, int home, int stepsPerUnit, bool isEnabled, double accel, double vel)
    {
        // hacky fix for PROD winder2
        AxisSetting axisSetting = new AxisSetting
        {
            Axis = axis,
            Enable = isEnabled,
            StepPin = step,
            DirPin = dir,
            StepNeg = false,
            DirNeg = false,
            MaxAccel = accel,            // max machine acceleration
            MaxVel = vel,                // max machine velocity
            StepPer = stepsPerUnit,
            HomePin = home,              // limit home pin
            HomeNeg = false,
            LimitPPin = 0,
            LimitNPin = 0,
            LimitNNeg = false,           // try setting this guy for the right limit switch, then wont have to poll\check
            SoftLimitP = 0,
            SoftLimitN = 0,
            SlaveAxis = 0,
            BacklashOn = false,
            BacklashDist = 0,
            CompAccel = 0,
            EnablePin = enable,
            EnablePinNeg = false,
            EnableDelay = 0,
            CurrentHiLowPin = 0,
            CurrentHiLowPinNeg = false,
            HomeBackOff = 0,
            RotaryAxis = false,
            RotaryRollover = false,
        };

        return axisSetting;
    }

    private bool isMotorRunning()
    {
        Stat s = new Stat { };
        int ret = GetStatus(ref s);
        return s.Idle == false;
    }

    private bool isMotorHoming()
    {
        Stat s = new Stat { };
        int ret = GetStatus(ref s);
        return s.Home == true;
    }

    public void makeCuts(int quantity, double length)
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return;
        }

        cutLength = length;
        cutQuantity = quantity;
        startCutThread();
    }

    private void startCutThread()
    {
        cancelTokenSource = new CancellationTokenSource();
        token = cancelTokenSource.Token;
        /* Test this impl.
        token.Register(() =>
        {
            Stop();
            return;
        });
        */
        Task task = new Task(doCutMove, token);
        task.Start();
    }

    private void doCutMove()
    {
        for (int i = 0; i < cutQuantity; i++)
        {
            // is main-thread saying to stop?
            if (token.IsCancellationRequested)
            {
                Stop();
                return;
            }

            // compensate for offset, move to cut
            if (useCamera)
            {
                doMotorMove(cameraOffset, true);
                while (isMotorRunning()) ; // NOP
            }

            // make sure we've moved
            Thread.Sleep(100);

            // first cut
            if (!bladeDown())
            {
                return;
            }

            // blade up settings
            Thread.Sleep(100);
            bladeUp();
            Thread.Sleep(100);

            // go back if using camera
            if (useCamera)
            {
                doMotorMove(cameraOffset, false);
                while (isMotorRunning()) ;
            }

            // move to next position
            doMotorMove(cutLength, true);
            while (isMotorRunning()) ; // NOP
        }
    }

    public void makeCutsWithVision(int quantity, double length, string msg)
    {
        if (!isConnected)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return;
        }

        cutLength = length;
        cutQuantity = quantity;
        startCutThreadWithVision();
    }

    private void startCutThreadWithVision()
    {
        cancelTokenSource = new CancellationTokenSource();
        token = cancelTokenSource.Token;
        Task task = new Task(doCutMoveWithVision, token);
        task.Start();
    }

    private void doCutMoveWithVision()
    {
        for (int i = 0; i < cutQuantity; i++)
        {
            if (token.IsCancellationRequested)
            {
                Stop();
                return;
            }

            // find the gap with the vison system
            if (!findGap())
            {
                MessageBox.Show("ERROR\nGap Not Found", "Z-Axis Connector Company");
                return;
            }
            //MessageBox.Show("Found Gap? Vision Offset: " + visionOffset.ToString());

            // update the cut-gap and make the cut
            // need to go pass-the cross hair
            // chop = no cam. move
            if (!updateCutAndDoChop())
            {
                return;
            }

            // wait for motor move to complete
            while (isMotorRunning()) ;
        }
    }

    private bool findGap()
    {
        bool check = true;
        if (!getVisionSceneDetails())
        {
            // see cmdFindGap_click in vba
            check = false;
            doMotorMove(0.01, false);
            doMotorMove(0.01, true);
            Thread.Sleep(50);
            for (int j = 0; j < 8; j++)
            {
                if (getVisionSceneDetails())
                {
                    check = true;
                    break;
                }
                //doMotorMove(0.03, true);
                doMotorMove(0.01, true);
                Thread.Sleep(50);
            }
        }

        return check;
    }

    private bool updateCutAndDoChop()
    {
        double dist_to_move;
        double cam_offset = Math.Abs(cameraOffset);

        // will need to check visionOffset has a valid value
        // double check this, moving in wrong direction
        // cut-move is from left-to-right, as the camoffset needs to move to the left
        dist_to_move = visionOffset + cam_offset - .01;
        //dist_to_move = visionOffset + cam_offset + .01; 
        //Thread.Sleep(2000); // extra delay for demo purposes
        //MessageBox.Show("moving to " + dist_to_move.ToString() + "\ncam_offset " + cam_offset.ToString() + "\nvision_offset " + visionOffset.ToString());
        doMotorMove(dist_to_move, true);    // TRUE -> move to left
        //Thread.Sleep(2000); // extra delay for demo purposes   
        doMotorMove(.01, true);             // TRUE -> move to left
        //doMotorMove(.01, false);             // TRUE -> move to right
        Thread.Sleep(80);                   // make sure we moved

        // make the chop
        if (!bladeDown())
        {
            //return false;
        }

        // blade up
        Thread.Sleep(100);
        bladeUp();
        Thread.Sleep(100);

        // move length - xhair to next part
        dist_to_move = cutLength - cam_offset;
        //MessageBox.Show("moving to " + dist_to_move.ToString() + "\ncam_offset " + cam_offset);
        doMotorMove(dist_to_move, true);

        return true;
    }

    private bool getVisionSceneDetails()
    {
        return true;
    }

    private void errorLoop()
    {
        while (true) ;   // NOP
    }

    public bool setOutputPins()
    {
        return true;
    }

    public bool closeDevice()
    {
        if (Close() != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        isConnected = false;

        return true;
    }

    public bool resetHome()
    {
        int ret = HomeOn(0, 1.0, .5, true);

        if (ret == (int)ReturnVal.UC100_MOVEMENT_IN_PROGRESS)
        {
            Stop();
            MessageBox.Show("Movement in Progress!\nHit 'Stop' then try again", "Z-Axis Connector Company");
            return false;
        }

        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(ret.ToString() + "\n" + helpStr, "Z-Axis Connector Company");
            return false;
        }

        while (isMotorHoming()) ;    // wait for motor to home
        doMotorMove(4.5, true);     // move to home position

        return true;
    }

    private bool getDeviceInfo()
    {
        int type = 0;
        int serialNumber = 0;
        int ret = DeviceInfo(boardID, ref type, ref serialNumber);
        return ret == (int)ReturnVal.UC100_OK;
    }

    private bool listDevices()
    {
        int devices = 0;
        return ListDevices(ref devices) == (int)ReturnVal.UC100_OK;
    }

    private bool deviceOpen()
    {
        return Open(1) == (int)ReturnVal.UC100_OK;
    }

    public void doMotorMove(double steps, bool dir)
    {
        double absSteps = Math.Abs(steps);
        int ret = AddLinearMoveRel(0, absSteps, 1, speed, dir);
        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
        }
    }

    public void stopMotor()
    {
        try
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
        }
        catch (Exception ex) { }
        Stop();
    }

    public bool disableMotor()
    {
        int ret;

        AxisSetting xAxisSetting = getAxisSetup(0, stepXPin, dirXPin, enaXPin, 0, xStepsPerUnit, false, 0.5, 1000);

        ret = SetAxisSetting(ref xAxisSetting);
        if (ret == (int)ReturnVal.UC100_MOVEMENT_IN_PROGRESS)
        {
            MessageBox.Show("Movement in Progress!\nHit 'Stop' then try again", "Z-Axis Connector Company");
            return false;
        }

        if (ret != (int)ReturnVal.UC100_OK)
        {
            MessageBox.Show(helpStr, "Z-Axis Connector Company");
            return false;
        }

        return true;
    }

}