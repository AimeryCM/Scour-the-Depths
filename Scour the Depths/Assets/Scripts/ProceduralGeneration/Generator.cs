using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration
{
	public class Generator : MonoBehaviour
	{
		private Graph graph = null;
		public List<Room> roomList = null;
		public Room startRoom = null;
		public int genHeight = 0;
		public int genWidth = 0;
		public Vector2Int startLocation = Vector2Int.zero;
		public readonly float halfCameraHeight = 5.625f;

		void Start()
		{
			graph = new Graph(startRoom, roomList, genWidth, genHeight, startLocation);
			Debug.Log("Graph Created");
			graph.PrintGraph();
			Debug.Log(graph.generationSeed);
			InstantiateRooms();
		}

		private void InstantiateRooms()
		{
			bool[,] covered = new bool[genHeight, genWidth];
			for(int y = 0; y < genHeight; y++)
			{
				for(int x = 0; x < genWidth; x++)
				{
					if(!covered[y,x])
					{
						covered[y,x] = true;
						Room room = graph.GetRoomFromMatrix(x, y);
						if(room != null)
						{
							InstantiateRoomAtCoords(x, y, room);
							for(int ycor = y; ycor < y + room.height; ycor++)
							{
								for(int xcor = x; xcor < x + room.width; xcor++)
								{
									covered[ycor, xcor] = true;
								}
							}
						}
					}
				}
			}
		}

		private GameObject InstantiateRoomAtCoords(int x, int y, Room room)
		{
			float halfCameraWidth = CalculateHalfCameraWidth();
			float xcoord = (x - startLocation.x) * (2 * halfCameraWidth);
			float ycoord = (y - startLocation.y) * (-2 * halfCameraHeight);
			return Instantiate(room.roomObject, new Vector3(xcoord, ycoord, 0), Quaternion.identity);
		}

		private float CalculateHalfCameraWidth()
		{
			return (halfCameraHeight / 4.5f) * 8f;
		}
	}
}
