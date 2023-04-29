using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using PhysX.VisualDebugger;
using System.Numerics;
using Vivid.Maths;

namespace Vivid.Physx
{
	public class BodyLink
	{
		public RigidActor Actor = null;
		public Vivid.Scene.Node Node= null;
	}
    public class SampleFilterShader : SimulationFilterShader
    {
        public override FilterResult Filter(int attributes0, FilterData filterData0, int attributes1, FilterData filterData1)
        {
            return new FilterResult
            {
                FilterFlag = FilterFlag.Callback,
                // Cause PhysX to report any contact of two shapes as a touch and call SimulationEventCallback.OnContact
                PairFlags = PairFlag.ContactDefault | PairFlag.NotifyTouchFound | PairFlag.NotifyTouchLost | PairFlag.NotifyContactPoints
            };
        }
    }
    public class PxCallback : SimulationEventCallback
	{

		public void Check(RigidActor act, System.Numerics.Vector3 pos, System.Numerics.Vector3 force,System.Numerics.Vector3 norm, RigidActor other)
		{
			if (QPhysics.ActorMap[act] != null)
			{
			//	QPhysics.ActorMap[act].OnCollision(new Vec3(pos.X, pos.Y, pos.Z), new Vec3(force.X,force.Y,force.Z),new Vec3(norm.X,norm.Y,norm.Z), QPhysics.ActorMap[other]);
            }
		}
        public override void OnContact(ContactPairHeader pairHeader, ContactPair[] pairs)
        {
			int a = 5;
			//Environment.Exit(1);

			//Check(pairHeader.Actor0,p
			//Check(pairHeader.Actor1);


            foreach (var pair in pairs)
            {

				var cp = pair.ExtractContacts();
				foreach(var c in cp)
				{

					Check(pairHeader.Actor0, c.Position, c.Impulse,c.Normal, pairHeader.Actor1);
					Check(pairHeader.Actor1, c.Position, c.Impulse, c.Normal, pairHeader.Actor0);

				}

				
				//Environment.Exit(1);

                //var actor1 = pair.
                //var actor2 = pair.ActorB;


                //if (actor1.Geometry is BoxGeometry && actor2.Geometry is BoxGeometry)
                {
                    // Two boxes have collided
                    // Do something here...
                }
            }
        }
    }
    public static class QPhysics
    {

        public static Foundation _Foundation;
		public static Physics _Physics = null;
		public static Pvd _Pvd = null;
		public static ErrorLog _errorLog;
		public static Cooking _Cooking;
		public static PhysX.Scene _Scene;
		public static Dictionary<RigidActor,Vivid.Scene.Node> ActorMap = new Dictionary<RigidActor,Vivid.Scene.Node>();
		public static void AddActor(RigidActor act,Vivid.Scene.Node node)
		{

			BodyLink bl = new BodyLink();
			bl.Actor = act;
			bl.Node = node;
			ActorMap[act] = node;
			_Scene.AddActor(act);
			

		}
        public static void InitPhysics()
        {
			_errorLog = new ErrorLog();
			_Foundation = new Foundation(_errorLog);
			_Pvd = new Pvd(_Foundation);
			_Pvd.Connect("127.0.0.1");


			_Physics = new Physics(_Foundation,true, _Pvd);

			CookingParams cook = new CookingParams();

			

			//cook.MidphaseDesc = mpd;

		

			var sceneDesc = new SceneDesc();
            sceneDesc.FilterShader = new SampleFilterShader();

            sceneDesc.Gravity = new System.Numerics.Vector3(0.0f, -9.81f, 0.0f);
			sceneDesc.Flags |= SceneFlag.EnableCcd;
			sceneDesc.CCDMaxPasses = 4;
			sceneDesc.SolverType = SolverType.PGS;

            cb = new PxCallback();
            //sceneDesc.SimulationEventCallback = cb;
	
			//sceneDesc.Flags |= SceneFlag.EnablePcm;
	
			

			_Scene = _Physics.CreateScene(sceneDesc);															   
			//_Pvd.Connect()
			//_Cooking = _Physics.CreateCooking(cook);
			_Cooking = _Physics.CreateCooking();
			//_Pvd.IsConnected;

		//	_Scene.SetSimulationEventCallback(cb);
	


			//_Physics.CreateScene();

		}
		static SimulationEventCallback cb;



		public static void Hit(RaycastHit[] hit,bool h)
		{

		}
        public static bool Raycast(Vector3 origin, Vector3 dest, PXBody ignore)
        {
            Vector3 start = new Vector3(origin.X, origin.Y, origin.Z);
            Vector3 end = new Vector3(dest.X, dest.Y, dest.Z);
            Vector3 dir = end - start;
            float dist = dir.Length();
            dir = Vector3.Normalize(dir);

            Func<PhysX.RaycastHit[], bool> hitFilter = (hits) =>
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].Actor == ignore.DynamicBody)
                    {
                        return false;
                    }
                }

                return true; // The raycast did not hit any valid objects
            };

			if (_Scene.Raycast(start, dir, dist, 255, hitFilter, HitFlag.MeshBothSides))
            {
                Console.WriteLine("                           Hit!");
                return true;
            }

            return false;
        }
        public static void Simulate(float time)
        {
			/*
			 * if (_Pvd.IsConnected(true))
			{
				Console.WriteLine("PC: Yes");
			}
            else
            {
				Console.WriteLine("PC: No");
            }
			*/
				_Scene.Simulate(time);
			_Scene.FetchResults(true);
			//_Scene.
			
        }

    }

	public class ErrorLog : PhysX.ErrorCallback
	{
		private List<string> _errors;

		public ErrorLog()
		{
			_errors = new List<string>();
		}

		public override void ReportError(ErrorCode errorCode, string message, string file, int lineNumber)
		{
			string e = string.Format("Code: {0}, Message: {1}", errorCode, message);

			_errors.Add(e);

			Trace.WriteLine(e);
		}

		public override string ToString()
		{
			if (_errors.Count == 0)
				return "No errors";
			else
				return string.Format("{0} errors. Last error: {1}", _errors.Count, _errors[_errors.Count - 1]);
		}

		public int ErrorCount
		{
			get
			{
				return _errors.Count;
			}
		}

		public bool HasErrors
		{
			get
			{
				return _errors.Any();
			}
		}

		public string LastError
		{
			get
			{
				return _errors.LastOrDefault() ?? String.Empty;
			}
		}
	}


}
