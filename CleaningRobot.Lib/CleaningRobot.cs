using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class CleaningRobot
    {
        protected string[][] Map { get; set; }
        protected List<Coordinate> Visited { get; set; } = new List<Coordinate>();
        protected List<Coordinate> Cleaned { get; set; } = new List<Coordinate>();
        protected Battery Battery { get; set; }
        protected Coordinate CurrentCoordinate { get; set; }
        protected Coordinate PreviousCoordinate { get; set; }
        protected Facing CurrentFacing { get; set; }
        protected Action CurrentAction { get; set; }
        protected bool IsRunningBackOffStrategy { get; set; }
        private void Init()
        {
            Battery = new Battery();
            CurrentCoordinate = new Position();
        }
        protected bool IsObstacle() => (
                                        CurrentCoordinate.X < 0 ||
                                        CurrentCoordinate.Y < 0 ||
                                        CurrentCoordinate.X >= Map.GetLength(0) ||
                                        CurrentCoordinate.Y >= Map.GetLength(0) ||
                                        CurrentCoordinate.X > Map[CurrentCoordinate.Y].Length ||
                                        Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "C" ||
                                        Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "null" ||
                                        Battery.Status - Battery.Consumption(CurrentAction) < 0);

        private bool IsValidRequest(Request request)
        {
            return
                request == null || request.Battery == 0 ||
                request.Commands == null || request.Commands.Length == 0 ||
                request.Commands.Any(m => string.IsNullOrWhiteSpace(m)) ||
                !request.Commands.Any(m => Rules.Commands.Contains(m)) ||
                request.Map == null || request.Start == null || !Rules.CheckMap(request.Map);
        }

        private void ExecCommand()
        {
            if (Battery.Status - Battery.Consumption(CurrentAction) < 0)
            {
                return;
            }

            Battery.Status = Battery.Status - Battery.Consumption(CurrentAction);

            if (CurrentAction == Action.A || CurrentAction == Action.B)
            {
                PreviousCoordinate = CurrentCoordinate;
                CurrentCoordinate = GetCurrentCoordinate(CurrentFacing, CurrentAction, CurrentCoordinate);
            }
            else if (CurrentAction == Action.TL || CurrentAction == Action.TR)
            {
                CurrentFacing = (Facing)Enum.Parse(typeof(Facing), FacingAction.Result[(int)CurrentFacing][(int)CurrentAction]);
            }

            if (!IsObstacle())
            {
                if ((CurrentAction == Action.A || CurrentAction == Action.B) && (!Visited.Any(m => m.X == CurrentCoordinate.X && m.Y == CurrentCoordinate.Y)))
                {
                    Visited.Add(CurrentCoordinate);
                }
                else if ((CurrentAction == Action.C) && (!Cleaned.Any(m => m.X == CurrentCoordinate.X && m.Y == CurrentCoordinate.Y)))
                {
                    Cleaned.Add(CurrentCoordinate);
                }
            }
        }
        private Coordinate GetCurrentCoordinate(Facing facing, Action action, Coordinate coordinate)
        {
            if ((facing == Facing.N && action == Action.B) || (facing == Facing.S && action == Action.A))
            {
                return new Coordinate { X = coordinate.X, Y = coordinate.Y + 1 };
            }

            if ((facing == Facing.N && action == Action.A) || (facing == Facing.S && action == Action.B))
            {
                return new Coordinate { X = coordinate.X, Y = coordinate.Y - 1 };
            }

            if ((facing == Facing.E && action == Action.A) || (facing == Facing.W && action == Action.B))
            {
                return new Coordinate { X = coordinate.X + 1, Y = coordinate.Y };
            }

            if ((facing == Facing.E && action == Action.B) || (facing == Facing.W && action == Action.A))
            {
                return new Coordinate { X = coordinate.X - 1, Y = coordinate.Y };
            }

            return coordinate;
        }

        public Result Run(Request request)
        {
            if (IsValidRequest(request))
            {
                return null;
            }

            Init();
            var result = new Result();
            Map = request.Map;
            Battery = new Battery { Status = request.Battery };
            CurrentCoordinate = request.Start;
            PreviousCoordinate = CurrentCoordinate;
            CurrentFacing = (Facing)Enum.Parse(typeof(Facing), request.Start.Facing);
            Visited.Add(new Coordinate { X = CurrentCoordinate.X, Y = CurrentCoordinate.Y });

            for (int i = 0; i < request.Commands.Length; i++)
            {
                CurrentAction = (Action)Enum.Parse(typeof(Action), request.Commands[i]);
                ExecCommand();

                if (IsObstacle())
                {
                    CurrentCoordinate = PreviousCoordinate;
                    IsRunningBackOffStrategy = true;
                    for (int j = 0; j < AlternativeActions.Path.Length; j++)
                    {
                        if (IsRunningBackOffStrategy)
                        {
                            for (int k = 0; k < AlternativeActions.Path[j].Length; k++)
                            {
                                CurrentAction = (Action)Enum.Parse(typeof(Action), AlternativeActions.Path[j][k]);
                                ExecCommand();
                            }

                            if (!IsObstacle())
                            {
                                IsRunningBackOffStrategy = false;
                                break;
                            }
                            else
                            {
                                CurrentCoordinate = PreviousCoordinate;
                                IsRunningBackOffStrategy = true;
                            }
                        }
                    }
                }
            }
            
            result.Visited = Visited.OrderBy(m => m.X).ThenBy(m => m.Y).ToList();
            result.Cleaned = Cleaned.OrderBy(m => m.X).ThenBy(m => m.Y).ToList();
            result.Final = new Position { X = CurrentCoordinate.X, Y = CurrentCoordinate.Y, Facing = CurrentFacing.ToString() };
            result.Battery = Battery.Status;

            return result;
        }
    }
}
