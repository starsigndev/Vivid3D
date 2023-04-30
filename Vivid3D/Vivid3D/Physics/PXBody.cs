﻿using PhysX;
using System.Numerics;
using Vivid.Maths;

namespace Vivid.Physx
{
    public enum BodyType
    {
        Box, Sphere, TriMesh, ConvexHull, None, FPS
    }

    public class PXBody
    {
        public RigidStatic StaticBody
        {
            get;
            set;
        }

        public RigidDynamic DynamicBody
        {
            get;
            set;
        }

        public RigidActor Body
        {
            get;
            set;
        }

        public Shape Shape
        {
            get;
            set;
        }

        public PhysX.Material Material
        {
            get;
            set;
        }

        public PXBody()
        {
            //  InitBody();
        }

        public void Constrain(bool x, bool y, bool z)
        {
            DynamicBody.RigidDynamicLockFlags = RigidDynamicLockFlags.AngularX | RigidDynamicLockFlags.AngularZ;
        }

        public Vivid.Scene.Node Node
        {
            get;
            set;
        }

        //private OpenTK.Mathematics.Matrix4
        public static Quaternion GLMRotationToQuaternion(OpenTK.Mathematics.Matrix4 glmRot)
        {
            Vector3 axis;
            float angle;

            // Extract the axis-angle representation of the rotation
            if (glmRot[2, 1] > 0.999f)
            {
                // Singularity at north pole
                axis = new Vector3(1, 0, 0);
                angle = 0;
            }
            else if (glmRot[2, 1] < -0.999f)
            {
                // Singularity at south pole
                axis = new Vector3(1, 0, 0);
                angle = (float)Math.PI;
            }
            else
            {
                axis = new Vector3(glmRot[1, 2] - glmRot[2, 1], glmRot[2, 0] - glmRot[0, 2], glmRot[0, 1] - glmRot[1, 0]);
                angle = (float)Math.Acos((glmRot[0, 0] + glmRot[1, 1] + glmRot[2, 2] - 1) / 2);
            }

            // Convert the axis-angle representation to a quaternion
            float sinHalfAngle = (float)Math.Sin(angle / 2);
            float cosHalfAngle = (float)Math.Cos(angle / 2);

            Vector3 ap = axis * sinHalfAngle;

            return new Quaternion(new Vector3(ap.X, ap.Y, ap.Z), cosHalfAngle);
        }

        public static Matrix4x4 ConvertMatrix4ToMatrix4x4(OpenTK.Mathematics.Matrix4 matrix)
        {
            Matrix4x4 physxMatrix = new Matrix4x4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    physxMatrix[i, j] = matrix[i, j];
                }
            }

            return physxMatrix;
        }

        public void SetPose(OpenTK.Mathematics.Matrix4 world)
        {
            Body.GlobalPose = ConvertMatrix4ToMatrix4x4(world);
        }

        public void SetPose(OpenTK.Mathematics.Vector3 pos, OpenTK.Mathematics.Matrix4 rot)
        {
            //$$$

            //var q = rot.ExtractRotation();
            // var pose = System.Numerics.Matrix4x4.CreateTranslation(new System.Numerics.Vector3(pos.x, pos.y, pos.z));
            //Body.GlobalPosePosition = new System.Numerics.Vector3(-pos.x, pos.y, pos.z);
            //Body.GlobalPoseQuat =GLMRotationToQuaternion( rot.Inverse);
            Body.GlobalPose = ConvertMatrix4ToMatrix4x4(rot);

            //Body.GlobalPoseQuat = new System.Numerics.Quaternion(q.X, q.Y, q.Z, q.W);

            //Body.GlobalPose = pose;
        }

        public void MakeStatic()
        {
            //cBody.RigidDynamicLockFlags = RigidDynamicLockFlags.LinearX | RigidDynamicLockFlags.LinearY | RigidDynamicLockFlags.LinearZ | RigidDynamicLockFlags.AngularX | RigidDynamicLockFlags.AngularY | RigidDynamicLockFlags.AngularZ;
            //DynamicBody.RigidDynamicLockFlags
        }

        public virtual void InitBody()
        {
        }

        public Vector3 GetPos()
        {
            var pose = Body.GlobalPosePosition;
            return new Vector3(-pose.X, pose.Y, pose.Z);
            //  return new OpenTK.Mathematics.Vector3(pose.X, pose.Y, pose.Z);
        }

        public static Quaternion AddNegative180PitchRotation(Quaternion quat)
        {
            float angle = MathHelp.pi;
            float halfAngle = angle * 0.5f;
            float sin = (float)Math.Sin(halfAngle);
            float cos = (float)Math.Cos(halfAngle);

            float qx = quat.X;
            float qy = quat.Y;
            float qz = quat.Z;
            float qw = quat.W;

            // Rotate around X axis
            float rx = sin * qw + cos * qx;
            float ry = cos * qy - sin * qz;
            float rz = cos * qz + sin * qy;
            float rw = cos * qw - sin * qx;

            return new Quaternion(rx, ry, rz, rw);
        }

        public OpenTK.Mathematics.Matrix4 GetRot()
        {
            var q = Body.GlobalPoseQuat;
            var mat = Body.GlobalPose;

            //return new Matrix4(new Quat(q.X, q.Y, q.Z, q.W));

            //  return new OpenTK.Mathematics.Matrix4(AddNegative180PitchRotation(new Quaternion(q.X, q.Y, q.Z, q.W)));
            return OpenTK.Mathematics.Matrix4.Identity;
        }

        public float W, H, D;
    }
}