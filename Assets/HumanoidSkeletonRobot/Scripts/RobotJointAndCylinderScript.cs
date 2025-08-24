using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJointAndCylinderScript : MonoBehaviour
{
    public int jointCount = 2;
    public Transform[] jointTransforms;
    public Transform[] jointLookAtTransforms;
    public Transform[] innerJointTransforms;
    
    public int cylinderCount = 62;
    public Transform[] looAtFromTransforms;
    public Transform[] looAtToTransforms;
   
    // Start is called before the first frame update
    void Start()
    {
        jointTransforms = new Transform[jointCount];
        jointLookAtTransforms = new Transform[jointCount];
        innerJointTransforms = new Transform[jointCount];
    
        jointTransforms[0] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.006");
        jointLookAtTransforms[0] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L");
        innerJointTransforms[0] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/LowerArm_L.005");

        jointTransforms[1] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.006");
        jointLookAtTransforms[1] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R");
        innerJointTransforms[1] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/LowerArm_R.005");


        looAtFromTransforms = new Transform[cylinderCount];
        looAtToTransforms = new Transform[cylinderCount];

        looAtFromTransforms[0] = transform.Find("Root/Pelvis/Pelvis_L");
        looAtFromTransforms[1] = transform.Find("Root/Pelvis/Pelvis_L.001");
        looAtFromTransforms[2] = transform.Find("Root/Pelvis/Pelvis_L.002");
        looAtFromTransforms[3] = transform.Find("Root/Pelvis/Pelvis_L.003");
        looAtFromTransforms[4] = transform.Find("Root/Pelvis/Pelvis_L.004");
        looAtFromTransforms[5] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L");
        looAtFromTransforms[6] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.005");
        looAtFromTransforms[7] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.002");
        looAtFromTransforms[8] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.004");
        looAtFromTransforms[9] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.001");
        looAtFromTransforms[10] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.006");
        looAtFromTransforms[11] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.001");
        looAtFromTransforms[12] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.003");
        looAtFromTransforms[13] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.005");
        looAtFromTransforms[14] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_L.001");
        looAtFromTransforms[15] = transform.Find("Root/Pelvis/UpperLeg_L/UpperLeg_L.004");
        looAtFromTransforms[16] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/LowerLeg_L.002");
        looAtFromTransforms[17] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/LowerLeg_L.003");
        looAtFromTransforms[18] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/LowerLeg_L.004");
        looAtFromTransforms[19] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_L.002");
        looAtFromTransforms[20] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.006");
        looAtFromTransforms[21] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.007");
        looAtFromTransforms[22] = transform.Find("Root/Pelvis/Spine/Spine_L");
        looAtFromTransforms[23] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.008");
        looAtFromTransforms[24] = transform.Find("Root/Pelvis/Spine/Spine_L.001");
        looAtFromTransforms[25] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/LowerArm_L.002");
        looAtFromTransforms[26] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/LowerArm_L.003");
        looAtFromTransforms[27] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/LowerArm_L.004");
        looAtFromTransforms[28] = transform.Find("Root/Pelvis/UpperLeg_L/UpperLeg_L.005");
        looAtFromTransforms[29] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/Foot_L/Foot_L.004");

        looAtFromTransforms[30] = transform.Find("Root/Pelvis/Pelvis_R");
        looAtFromTransforms[31] = transform.Find("Root/Pelvis/Pelvis_R.001");
        looAtFromTransforms[32] = transform.Find("Root/Pelvis/Pelvis_R.002");
        looAtFromTransforms[33] = transform.Find("Root/Pelvis/Pelvis_R.003");
        looAtFromTransforms[34] = transform.Find("Root/Pelvis/Pelvis_R.004");
        looAtFromTransforms[35] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R");
        looAtFromTransforms[36] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.005");
        looAtFromTransforms[37] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.002");
        looAtFromTransforms[38] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.004");
        looAtFromTransforms[39] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.001");
        looAtFromTransforms[40] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.006");
        looAtFromTransforms[41] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.001");
        looAtFromTransforms[42] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.003");
        looAtFromTransforms[43] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.005");
        looAtFromTransforms[44] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_R.001");
        looAtFromTransforms[45] = transform.Find("Root/Pelvis/UpperLeg_R/UpperLeg_R.004");
        looAtFromTransforms[46] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/LowerLeg_R.002");
        looAtFromTransforms[47] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/LowerLeg_R.003");
        looAtFromTransforms[48] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/LowerLeg_R.004");
        looAtFromTransforms[49] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_R.002");
        looAtFromTransforms[50] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.006");
        looAtFromTransforms[51] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.007");
        looAtFromTransforms[52] = transform.Find("Root/Pelvis/Spine/Spine_R");
        looAtFromTransforms[53] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.008");
        looAtFromTransforms[54] = transform.Find("Root/Pelvis/Spine/Spine_R.001");
        looAtFromTransforms[55] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/LowerArm_R.002");
        looAtFromTransforms[56] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/LowerArm_R.003");
        looAtFromTransforms[57] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/LowerArm_R.004");
        looAtFromTransforms[58] = transform.Find("Root/Pelvis/UpperLeg_R/UpperLeg_R.005");
        looAtFromTransforms[59] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/Foot_R/Foot_R.004");

        looAtFromTransforms[60] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck.001");
        looAtFromTransforms[61] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck.002");

        
        looAtToTransforms[0] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.001");
        looAtToTransforms[1] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.002");
        looAtToTransforms[2] = transform.Find("Root/Pelvis/UpperLeg_L/UpperLeg_L.001");
        looAtToTransforms[3] = transform.Find("Root/Pelvis/UpperLeg_L/UpperLeg_L.002");
        looAtToTransforms[4] = transform.Find("Root/Pelvis/UpperLeg_L/UpperLeg_L.003");
        looAtToTransforms[5] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.002");
        looAtToTransforms[6] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/Shoulder_L.003");
        looAtToTransforms[7] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck_L.001");
        looAtToTransforms[8] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_L.004");
        looAtToTransforms[9] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.004");
        looAtToTransforms[10] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.003");
        looAtToTransforms[11] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/LowerArm_L.001");
        looAtToTransforms[12] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_L");
        looAtToTransforms[13] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck_L");
        looAtToTransforms[14] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Jaw/Jaw_L");
        looAtToTransforms[15] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/LowerLeg_L.001");
        looAtToTransforms[16] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/Foot_L/Foot_L.001");
        looAtToTransforms[17] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/Foot_L/Foot_L.002");
        looAtToTransforms[18] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/Foot_L/Foot_L.003");
        looAtToTransforms[19] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Jaw/Jaw_L.001");
        looAtToTransforms[20] = transform.Find("Root/Pelvis/Spine/Chest/Chest_L");
        looAtToTransforms[21] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/UpperArm_L.005");
        looAtToTransforms[22] = transform.Find("Root/Pelvis/Pelvis_L.005");
        looAtToTransforms[23] = transform.Find("Root/Pelvis/Spine/Chest/Chest_L.001");
        looAtToTransforms[24] = transform.Find("Root/Pelvis/Pelvis_L.006");
        looAtToTransforms[25] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/Hand_L.001");
        looAtToTransforms[26] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/Hand_L.002");
        looAtToTransforms[27] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/Hand_L.003");
        looAtToTransforms[28] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/LowerLeg_L.005");
        looAtToTransforms[29] = transform.Find("Root/Pelvis/UpperLeg_L/LowerLeg_L/Foot_L/Toe_L/Toe_L.001");

        looAtToTransforms[30] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.001");
        looAtToTransforms[31] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.002");
        looAtToTransforms[32] = transform.Find("Root/Pelvis/UpperLeg_R/UpperLeg_R.001");
        looAtToTransforms[33] = transform.Find("Root/Pelvis/UpperLeg_R/UpperLeg_R.002");
        looAtToTransforms[34] = transform.Find("Root/Pelvis/UpperLeg_R/UpperLeg_R.003");
        looAtToTransforms[35] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.002");
        looAtToTransforms[36] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/Shoulder_R.003");
        looAtToTransforms[37] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck_R.001");
        looAtToTransforms[38] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/UpperChest_R.004");
        looAtToTransforms[39] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.004");
        looAtToTransforms[40] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.003");
        looAtToTransforms[41] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/LowerArm_R.001");
        looAtToTransforms[42] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head_R");
        looAtToTransforms[43] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Neck_R");
        looAtToTransforms[44] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Jaw/Jaw_R");
        looAtToTransforms[45] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/LowerLeg_R.001");
        looAtToTransforms[46] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/Foot_R/Foot_R.001");
        looAtToTransforms[47] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/Foot_R/Foot_R.002");
        looAtToTransforms[48] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/Foot_R/Foot_R.003");
        looAtToTransforms[49] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Jaw/Jaw_R.001");
        looAtToTransforms[50] = transform.Find("Root/Pelvis/Spine/Chest/Chest_R");
        looAtToTransforms[51] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/UpperArm_R.005");
        looAtToTransforms[52] = transform.Find("Root/Pelvis/Pelvis_R.005");
        looAtToTransforms[53] = transform.Find("Root/Pelvis/Spine/Chest/Chest_R.001");
        looAtToTransforms[54] = transform.Find("Root/Pelvis/Pelvis_R.006");
        looAtToTransforms[55] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/Hand_R.001");
        looAtToTransforms[56] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/Hand_R.002");
        looAtToTransforms[57] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/Hand_R.003");
        looAtToTransforms[58] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/LowerLeg_R.005");
        looAtToTransforms[59] = transform.Find("Root/Pelvis/UpperLeg_R/LowerLeg_R/Foot_R/Toe_R/Toe_R.001");

        looAtToTransforms[60] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Head.001");
        looAtToTransforms[61] = transform.Find("Root/Pelvis/Spine/Chest/UpperChest/Neck/Head/Jaw/Jaw.001");
    }

    void FixedUpdate()
    {
        for (int i = 0; i < cylinderCount; i++)
        {
            looAtFromTransforms[i].LookAt(looAtToTransforms[i].position);
            looAtToTransforms[i].LookAt(looAtFromTransforms[i].position);
        }

        for (int i = 0; i < jointCount; i++)
        {

            Vector3 initialJointX = jointTransforms[i].right;
            Vector3 initialJointY = jointTransforms[i].up;
            Vector3 initialJointZ = jointTransforms[i].forward;

            Vector3 initialInnerJointX = innerJointTransforms[i].right;
            Vector3 initialInnerJointY = innerJointTransforms[i].up;

            float initialJointRotX = jointTransforms[i].localEulerAngles.x;
            float initialJointRotY = jointTransforms[i].localEulerAngles.y;
            float initialJointRotZ = jointTransforms[i].localEulerAngles.z;

            float initialInnerJointRotX = innerJointTransforms[i].localEulerAngles.x;
            float initialInnerJointRotY = innerJointTransforms[i].localEulerAngles.y;
            float initialInnerJointRotZ = innerJointTransforms[i].localEulerAngles.z;

            Vector3 jointVector3 = jointLookAtTransforms[i].position - jointTransforms[i].position;
            Vector3 normalVector3 = Vector3.Cross(jointVector3, initialJointZ);
            float angleZ = Vector3.Angle(initialJointY, normalVector3);

            jointTransforms[i].localRotation = Quaternion.Euler(initialJointRotX, initialJointRotY, initialJointRotZ - angleZ * Mathf.Sign(Vector3.Dot(normalVector3, initialJointX)));
            float innerAngleZ = Vector3.Angle(initialInnerJointY, normalVector3);
            innerJointTransforms[i].localRotation = Quaternion.Euler(initialInnerJointRotX, initialInnerJointRotY, initialInnerJointRotZ - innerAngleZ * Mathf.Sign(Vector3.Dot(normalVector3, initialInnerJointX)));
        }
    }
}
