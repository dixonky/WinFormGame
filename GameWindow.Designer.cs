namespace FGame
{
     partial class GameWindow
     {
          /// <summary>
          /// Required designer variable.
          /// </summary>
          private System.ComponentModel.IContainer components = null;

          /// <summary>
          /// Clean up any resources being used.
          /// </summary>
          /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
          protected override void Dispose(bool disposing)
          {
               if (disposing && (components != null))
               {
                    components.Dispose();
               }
               base.Dispose(disposing);
          }

          #region Windows Form Designer generated code

          /// <summary>
          /// Required method for Designer support - do not modify
          /// the contents of this method with the code editor.
          /// </summary>
          private void InitializeComponent()
          {
               this.components = new System.ComponentModel.Container();
               this.tmrMoving = new System.Windows.Forms.Timer(this.components);
               this.playerStatus = new System.Windows.Forms.Label();
               this.SuspendLayout();
               // 
               // tmrMoving
               // 
               this.tmrMoving.Enabled = true;
               this.tmrMoving.Tick += new System.EventHandler(this.tmrMoving_Tick);
               // 
               // playerStatus
               // 
               this.playerStatus.AutoSize = true;
               this.playerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.playerStatus.Location = new System.Drawing.Point(12, 9);
               this.playerStatus.Name = "playerStatus";
               this.playerStatus.Size = new System.Drawing.Size(0, 25);
               this.playerStatus.TabIndex = 0;
               // 
               // GameWindow
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(1222, 1042);
               this.Controls.Add(this.playerStatus);
               this.DoubleBuffered = true;
               this.Name = "GameWindow";
               this.Text = "Game";
               this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameWindow_Paint);
               this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
               this.Resize += new System.EventHandler(this.GameWindow_ResizeEnd);
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion
          private System.Windows.Forms.Timer tmrMoving;
          private System.Windows.Forms.Label playerStatus;
     }
}