using Godot;
using System;

public partial class Penguin : Sprite2D
{
	#region input
		private Vector2 input;
		private Vector2 lastInput;
	
		private void GatherInput() {
			input = Input.GetVector("Left", "Right", "Up", "Down", 0f); //Gets the input
			if (input.X != 0f || input.Y != 0f){ //changes lastInput only if input is not (0,0)
				lastInput.X = input.X;
				lastInput.Y = input.Y;
			}
		}
	#endregion
	
	#region velocity
		private Vector2 velocity; //Vector2 that will be applied to Position
		private float speed = 5f; //Here is the speed, the one that shoud be changed if someone change the penguin's speed
		private float decelerationCoeficient = 4f; //the coeficient for deceleration
		
		private void CalculateVelocity(float del){
			if (input.X != 0f || input.Y != 0f){ //here velocity is calculated
				velocity.X = input.X * speed;
				velocity.Y = input.Y * speed;
			}
			else{ //here deceleration is calculated
				if (decelerationCoeficient * del < Mathf.Abs(velocity.X) && lastInput.X != 0f){
					if (velocity.X < 0f){
						velocity.X += decelerationCoeficient * del;
					}
					else {
						velocity.X -= decelerationCoeficient * del;
					}
				}
				else if (lastInput.X != 0f){
					velocity.X = 0f;
				}
				if (decelerationCoeficient * del < Mathf.Abs(velocity.Y) && lastInput.Y != 0f){
					if (velocity.Y < 0f){
						velocity.Y += decelerationCoeficient * del;
					}
					else {
						velocity.Y -= decelerationCoeficient * del;
					}
				}
				else if (lastInput.Y != 0f){
					velocity.Y = 0f;
				}
			}
		}
	#endregion
	
	#region animation
		private void ChangeAnimation(Vector2 inp){ //For changing the frame depending on input value
			if (inp.Y < 0){
				this.Frame = 2;
				return;
			}
			else if (inp.Y > 0){
				this.Frame = 0;
				return;
			}
			if (inp.X > 0){
				this.Frame = 3;
			}
			else if (inp.X < 0){
				this.Frame = 1;
			}
		}
	#endregion
	
	#region process
		public override void _PhysicsProcess(double delta){ //here godot gets input, calculate velocity and change position, also change the frame...
			//Due to changing position must be fluid, PhysicsProcess is used instead of Process
			this.GatherInput();
			this.ChangeAnimation(input);
			this.CalculateVelocity((float)delta);
			this.Position += velocity;
		}
	#endregion
}
