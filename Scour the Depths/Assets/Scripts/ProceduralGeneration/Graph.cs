using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration
{
	public class Graph
	{
		private Vertex start = null;
		private List<Room> roomList = null;
		private Vertex[,] roomMatrix = null;	//[rows, columns], true if occupied, false if open
		private readonly Vector2Int DEFAULT_START_LOCATION = Vector2Int.zero;
		private int roomListIndex = -1;
		public readonly Int32 generationSeed; 

		public Graph(Room startRoom, List<Room> roomList, int genWidth, int genHeight, Vector2Int startLocation)
		{
			roomMatrix = new Vertex[genHeight, genWidth];
			if(CheckLocationValidity(startLocation, startRoom)){
				start = AddVertex(startRoom, startLocation);
			}else
				start = AddVertex(startRoom, DEFAULT_START_LOCATION);
			this.roomList = roomList;
			System.Random rand = new System.Random();
			generationSeed = rand.Next();
			Debug.Log(GenerateRecursive(start) ? "Graph Recursion Success" : "Graph Recursion Failure");
		}

		private bool GenerateRecursive(Vertex current)
		{
			if(current == null)
				return false;
			bool[] currentDoors = current.GetRoom().doors;
			for(int index = 0; index < currentDoors.Length; index++)
			{
				if(currentDoors[index] == true)
				{
					Direction directionAttempt;
					Vector2Int positionAttempt;
					if(index < current.GetRoom().width)	//North side
					{
						directionAttempt = Direction.North;
						positionAttempt = new Vector2Int(current.GetLocation().x + index, current.GetLocation().y - 1);
					}
					else if(index < current.GetRoom().width + current.GetRoom().height)	//east
					{
						directionAttempt = Direction.East;
						positionAttempt = new Vector2Int(current.GetLocation().x + current.GetRoom().width, current.GetLocation().y + index - current.GetRoom().width);
					}
					else if(index < current.GetRoom().width * 2 + current.GetRoom().height)	//south
					{
						directionAttempt = Direction.South;
						positionAttempt = new Vector2Int(current.GetLocation().x + (index - current.GetRoom().width - current.GetRoom().height), current.GetLocation().y + current.GetRoom().height);
					}
					else	//west
					{
						directionAttempt = Direction.East;
						positionAttempt = new Vector2Int(current.GetLocation().x - 1, current.GetLocation().y + (index - current.GetRoom().width * 2 - current.GetRoom().height));
					}

					Vertex vert = null;
					bool found = false;
					if(CheckLocationValidity(positionAttempt))
					{
						int[] randomIndices = GenerateRandomIndexArray();
						for(int randomIndicesIndex = 0; randomIndicesIndex < randomIndices.Length && !found; randomIndicesIndex++)
						{
							vert = CreateValidRoom(positionAttempt, directionAttempt, current, roomList[randomIndicesIndex]);
							if(vert != null)
							{
								if(GenerateRecursive(vert))
								{
									found = true;
								}
								else
								{
									RemoveVertex(vert);
								}
							}
						}
						if(!found)
							return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Returns a random room. If reset is true, it shuffles the list and returns the first item in the list. 
		/// If reset is false, then it returns the next room in the list, or null if all the rooms have been returned
		/// before reseting.
		/// </summary>
		/// <param name="reset">Whether to shuffle the list and restart the index from the beginning</param>
		/// <returns>A random list</returns>
		private Room GetRandomRoom(bool reset)
		{
			if(reset)
			{
				ShuffleRoomList();
				roomListIndex = 0;
			}
			else
			{
				roomListIndex++;
			}
			if(roomListIndex < roomList.Count)
				return roomList[roomListIndex];
			return null;
		}

		private void ShuffleRoomList()
		{
			System.Random rng = new System.Random(generationSeed);
			int n = roomList.Count;
			while(n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				Room temp = roomList[k];
				roomList[k] = roomList[n];
				roomList[n] = temp;
			}
		}

		/// <summary>
		/// Returns a new vertex if its placement is valid. If it is valid, also marks the covered area in roomMatrix
		/// </summary>
		/// <param name="room">The room to add</param>
		/// <param name="location">The Location of the vertex to be added</param>
		/// <returns>The added vertex</returns>
		private Vertex AddVertex(Room room, Vector2Int location)
		{
			if(CheckLocationValidity(location, new Vector2Int(location.x + room.width - 1, location.y + room.height - 1)))
			{
				Vertex result = new Vertex(room, location);
				MarkLocation(result);
				return result;
			}
			return null;
		}

		private void ConnectVertices(Vertex vertex1, Vertex vertex2)
		{
			vertex1.AddEdge(new Edge(vertex1, vertex2));
			vertex2.AddEdge(new Edge(vertex2, vertex1));
		}

		private Vertex AddAndConnectVertex(Room room2, Vector2Int location, Vertex connected)
		{
			Vertex result = AddVertex(room2, location);
			ConnectVertices(result, connected);
			return result;
		}

		/// <summary>
		/// Removes a vertex from roomMatrix DOES NOT remove edges (TODO)
		/// </summary>
		/// <param name="vert">The Vertex to remove</param>
		private void RemoveVertex(Vertex vert)
		{
			//TODO add in edge removal
			if(vert == null)
				return;
			for(int x = vert.GetLocation().x; x < vert.GetLocation().x + vert.GetRoom().width; x++)
			{
				for(int y = vert.GetLocation().y; y < vert.GetLocation().y + vert.GetRoom().height; y++)
				{
					roomMatrix[y,x] = null;
				}
			}
		}

		private bool MarkLocation(Vertex vertex)
		{
			if(!CheckLocationValidity(vertex.GetLocation(), vertex.GetRoom()))
				return false;
			
			for(int x = vertex.GetLocation().x; x < vertex.GetLocation().x + vertex.GetRoom().width; x++)
				for(int y = vertex.GetLocation().y; y < vertex.GetLocation().y + vertex.GetRoom().height; y++)
					roomMatrix[y, x] = vertex;
			
			return true;

		}

		//returns true if it is a valid room placement, false otherwise
		private bool CheckLocationValidity(Vector2Int topLeft, Vector2Int botRight)
		{
			if(roomMatrix == null)
				return false;
			
			if(topLeft.y >= 0 && topLeft.y < roomMatrix.GetLength(0) && topLeft.x >= 0 && topLeft.x < roomMatrix.GetLength(1))
			{
				if(botRight.y >= 0 && botRight.y < roomMatrix.GetLength(0) && botRight.x >= 0 && botRight.x < roomMatrix.GetLength(1))
				{
					if(topLeft.y <= botRight.y && topLeft.x <= botRight.x)
					{
						for(int x = topLeft.x; x <= botRight.x; x++)
						{
							for(int y = topLeft.y; y <= botRight.y; y++)
							{
								if(roomMatrix[y, x] != null)
									return false;
							}
						}
						return true;
					}
				}
			}

			return false;
		}

		private bool CheckLocationValidity(Vector2Int location)
		{
			return CheckLocationValidity(location, location);
		}

		private bool CheckLocationValidity(Vector2Int location, Room room)
		{
			return CheckLocationValidity(location, new Vector2Int(location.x + room.width - 1, location.y + room.height - 1));
		}

		/// <summary>
		/// Goes through the room and checks to see if any doors would be facing the boundaries of the geneartion space
		/// Also where the code for preventing false doors could be included if it is needed
		/// </summary>
		/// <param name="testRoom">The room to be tested</param>
		/// <param name="position">The position of the room in the map</param>
		/// <returns>true if the room is validly placed, false if it has a bad door</returns>
		private bool CheckDoors(Room testRoom, Vector2Int position)
		{
			//check the doors to see if they point inside the map (don't face boundary)
			int index = 0;
			if(position.y == 0)	//North boundary
				for(int x = index; x < testRoom.width; x++)
					if(testRoom.doors[x])
						return false;
			index += testRoom.width;
			if(position.x + testRoom.width == roomMatrix.GetLength(1))	//East boundary
				for(int x = index; x < index + testRoom.height; x++)
					if(testRoom.doors[x])
						return false;
			index += testRoom.height;
			if(position.y + testRoom.height == roomMatrix.GetLength(0))	//South boudary
				for(int x = index; x < testRoom.width + index; x++)
					if(testRoom.doors[x])
						return false;
			index += testRoom.width;
			if(position.x == 0)	//West boundary
				for(int x = index; x < index + testRoom.height; x++)
					if(testRoom.doors[x])
						return false;

			return true;	
		}

		/// <summary>
		/// Given a room roomToTest, tests all possible alignments of the room (slides against the given wall) to see if the room will have
		/// a door that lines up in a valid position. If a location is found, a vertex is created and returned. Otherwise null is returned.
		/// </summary>
		/// <param name="position">The location of the box the door faces</param>
		/// <param name="doorSide">The side of the box of the origin that the door is on</param>
		/// <param name="origin">The vertex to connect to edges to</param>
		/// <param name="roomToTest">The room to try</param>
		/// <returns></returns>
		private Vertex CreateValidRoom(Vector2Int position, Direction doorSide, Vertex origin, Room roomToTest)
		{
			Vector2Int currentTest;
			if((int)doorSide % 2 == 1)	//door is on east or west
			{
				for(int offset = 0; offset < roomToTest.height; offset++)
				{
					if(doorSide == Direction.West)
					{
						currentTest = new Vector2Int(position.x + (roomToTest.width - 1), position.y - offset);
						if(CheckLocationValidity(currentTest, roomToTest))	//check to make sure the room won't overlap
							if(roomToTest.doors[roomToTest.width + offset])	//check to make sure the room has doors that would line up
								if(CheckDoors(roomToTest, currentTest))	//check to make sure the doors of this new room can be filled / aren't facing the edge of the map or the wall of another room
									return AddAndConnectVertex(roomToTest, currentTest, origin);	//Creates the vertex with this room, adds it to the 2d bool array, and adds edges
					}
					else
					{							
						currentTest = new Vector2Int(position.x, position.y - offset);
						if(CheckLocationValidity(currentTest, roomToTest))
							if(roomToTest.doors[roomToTest.width * 2 + roomToTest.height * 2 - offset - 1])
								if(CheckDoors(roomToTest, currentTest))
									return AddAndConnectVertex(roomToTest, currentTest, origin);
					}
				}
			}
			else	//door is on North or South
			{
				for(int offset = 0; offset < roomToTest.width; offset++)
				{
					if(doorSide == Direction.South)
					{
						currentTest = new Vector2Int(position.x - offset, position.y);
						if(CheckLocationValidity(currentTest, roomToTest))
							if(roomToTest.doors[offset])
								if(CheckDoors(roomToTest, currentTest))
									return new Vertex(roomToTest, currentTest);
					}
					else
					{
						currentTest = new Vector2Int(position.x - offset, position.y - (roomToTest.height - 1));
						if(CheckLocationValidity(currentTest, roomToTest))
							if(roomToTest.doors[roomToTest.width * 2 + roomToTest.height - offset - 1])
								if(CheckDoors(roomToTest, currentTest))
									return AddAndConnectVertex(roomToTest, currentTest, origin);
					}
				}
			}
			return null;
		}

		private Vertex FindValidRoom(Vector2Int position, Direction doorSide, Vertex origin, bool reset)
		{
			Room foundRoom = null;
			Vector2Int currentTest;

			while(foundRoom == null)
			{
				foundRoom = GetRandomRoom(reset);
				if((int)doorSide % 2 == 1)	//door is on east or west
				{
					for(int offset = 0; offset < foundRoom.height; offset++)
					{
						if(doorSide == Direction.West)
						{
							currentTest = new Vector2Int(position.x + (foundRoom.width - 1), position.y - offset);
							if(CheckLocationValidity(currentTest, foundRoom))	//check to make sure the room won't overlap
								if(foundRoom.doors[foundRoom.width + offset])	//check to make sure the room has doors that would line up
									if(CheckDoors(foundRoom, currentTest))	//check to make sure the doors of this new room can be filled / aren't facing the edge of the map or the wall of another room
										return AddAndConnectVertex(foundRoom, currentTest, origin);	//Creates the vertex with this room, adds it to the 2d bool array, and adds edges
						}
						else
						{							
							currentTest = new Vector2Int(position.x, position.y - offset);
							if(CheckLocationValidity(currentTest, foundRoom))
								if(foundRoom.doors[foundRoom.width * 2 + foundRoom.height * 2 - offset - 1])
									if(CheckDoors(foundRoom, currentTest))
										return AddAndConnectVertex(foundRoom, currentTest, origin);
						}
					}
				}
				else	//door is on North or South
				{
					for(int offset = 0; offset < foundRoom.width; offset++)
					{
						if(doorSide == Direction.South)
						{
							currentTest = new Vector2Int(position.x - offset, position.y);
							if(CheckLocationValidity(currentTest, foundRoom))
								if(foundRoom.doors[offset])
									if(CheckDoors(foundRoom, currentTest))
										return new Vertex(foundRoom, currentTest);
						}
						else
						{
							currentTest = new Vector2Int(position.x - offset, position.y - (foundRoom.height - 1));
							if(CheckLocationValidity(currentTest, foundRoom))
								if(foundRoom.doors[foundRoom.width * 2 + foundRoom.height - offset - 1])
									if(CheckDoors(foundRoom, currentTest))
										return AddAndConnectVertex(foundRoom, currentTest, origin);
						}
					}
				}
				foundRoom = null;
			}

			return null;
		}

		/// <summary>
		/// Get the room from the specified location
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <returns>The room from the specified location</returns>
		public Room GetRoomFromMatrix(int x, int y)
		{
			return roomMatrix[y,x] != null ? roomMatrix[y,x].GetRoom() : null;
		}

		/// <summary>
		/// Generates an array containing a random order of indexes of size == roomList.Count
		/// </summary>
		private int[] GenerateRandomIndexArray()
		{
			System.Random rng = new System.Random(generationSeed);
			int[] result = new int[roomList.Count];
			for(int index = 0; index < result.Length; index++)
				result[index] = index;
			int n = result.Length;
			while(n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				int temp = result[k];
				result[k] = result[n];
				result[n] = temp;
			}
			return result;
		}

		public void PrintGraph()
		{
			//System.Text.StringBuilder builder = new System.Text.StringBuilder();
			for(int y = 0; y < roomMatrix.GetLength(0); y++)
			{
				for(int x = 0; x < roomMatrix.GetLength(1); x++)
				{
					System.Text.StringBuilder builder = new System.Text.StringBuilder();
					builder.Append("(");
					builder.Append(x);
					builder.Append(", ");
					builder.Append(y);
					builder.Append("): ");
					builder.Append(roomMatrix[y,x] == null ? "Empty" : ProjectUtil.ArrayToString(roomMatrix[y,x].GetRoom().doors));
					Debug.Log(builder.ToString());
				}
			}
		}
	}

	public class Vertex
	{
		private Room room = null;
		private List<Edge> edgeList = null;
		private Vector2Int roomLocation = Vector2Int.zero;	//the location of the top left corner of the room

		public Vertex()
		{
			edgeList = new List<Edge>();
		}

		public Vertex(Room room)
		{
			this.room = room;
			edgeList = new List<Edge>();
		}

		public Vertex(Room room1, Vector2Int location) : this(room1)
		{
			roomLocation = location;
		}

		public void AddEdge(Edge edge)
		{
			edgeList.Add(edge);
		}

		public bool RemoveEdge(Edge edge)
		{
			return edgeList.Remove(edge);
		}

		public void SetRoom(Room room)
		{
			this.room = room;
		}

		public Room GetRoom()
		{
			return room;
		}

		public Vector2Int GetLocation()
		{
			return roomLocation;
		}

		public List<Edge> GetEdgeList()
		{
			return edgeList;
		}
	}

	public class Edge
	{
		private Vertex origin = null;
		private Vertex destination = null;

		public Edge(Vertex start, Vertex end)
		{
			origin = start;
			destination = end;
		}

		public Vertex GetOrigin()
		{
			return origin;
		}

		public Vertex GetDestination()
		{
			return destination;
		}
	}
}

/*


							do
							{
								vert = FindValidRoom(new Vector2Int(current.GetLocation().x + index, current.GetLocation().y - 1), Direction.North, current, firstTry);
								if(!GenerateRecursive(vert))
								{
									RemoveVertex(vert);
								}
								else
								{
									found = true;
								}
								firstTry = false;
							} while(vert != null && !found);
							if(vert == null)
								return false;
*/