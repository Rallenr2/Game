Public Class Form1
    Dim frame As Bitmap
    Dim framePen, formPen As Graphics
    Dim right As Boolean
    Dim left As Boolean
    Dim up As Boolean
    Dim down As Boolean
    Dim shipX, shipY As Integer
    Dim projectileY As Integer
    Dim projectileX As Integer
    Dim projectileB As Boolean
    Dim AlienX As Integer
    Dim alienDx As Integer = 8.5
    Dim AlienY As Integer
    Dim UFOx As Integer
    Dim UFOy As Integer
    Dim UFODx As Integer = 8.2
    Dim score As Integer

    Sub Enemy2()
        Dim MoveX, MoveY, MoveDx As Integer
        MoveDx = 8.4
        MoveX = MoveX + MoveDx
        MoveY = MoveY + MoveDx



    End Sub



    Private Sub GameTimer_Tick(sender As Object, e As EventArgs) Handles GameTimer.Tick
        'DRAW STUFF HERE
        '---------------
        Dim ShipLocation As Point
        ShipLocation.X = getWidth() / 2 - My.Resources.Ship.Width / 2
        ShipLocation.Y = getHeight() - My.Resources.Ship.Height

        Dim o As Integer
        Dim i As Integer
        Dim r As New Random

        i = r.Next(0, 400)
        o = r.Next(0, 400)

        'Draw your background first!
        framePen.DrawImage(My.Resources.oulzMx2__2_, 0, 0)
        'Scoring 
        framePen.DrawString("Score:" & score, New Font("Tahoma", 22, FontStyle.Regular), Brushes.Aqua, 0, 0)

        'Collisions and hitboxes
        framePen.DrawImage(My.Resources.Ship, shipX, shipY)
        Dim ProjectileBox As New Rectangle(projectileX, projectileY, My.Resources.Projectile.Width + 10 - My.Resources.Projectile.Width, My.Resources.Projectile.Height + 20)
        Dim AlienBox As New Rectangle(AlienX, AlienY, My.Resources.Ayylien.Width, My.Resources.Ayylien.Height)
        Dim UFOBox As New Rectangle(UFOx + 25, UFOy + 30, My.Resources.UFO.Width / 2, My.Resources.UFO.Height / 2)
        Dim ShipBox As New Rectangle(shipX + 23, shipY + 34, My.Resources.Ship.Width / 2, My.Resources.Ship.Height / 2)
        framePen.DrawRectangle(Pens.Red, ShipBox)
        framePen.DrawRectangle(Pens.Red, AlienBox)
        framePen.DrawRectangle(Pens.Red, UFOBox)
        If projectileB = True Then
            projectileY = projectileY - 30
            framePen.DrawImage(My.Resources.Projectile, projectileX, projectileY, 10, 50)
            framePen.DrawRectangle(Pens.White, ProjectileBox)
            If projectileY < 0 Then
                projectileB = False
            End If
            If ProjectileBox.IntersectsWith(UFOBox) Then
                UFOx = o
                UFOy = 0 - 150
                projectileB = False
                score = score + 100
                My.Computer.Audio.Play(My.Resources.Bullseye, AudioPlayMode.Background)
            End If
            If ProjectileBox.IntersectsWith(AlienBox) Then
                AlienX = i
                AlienY = 0 - 150
                projectileB = False
                score = score + 100
                My.Computer.Audio.Play(My.Resources.Bullseye, AudioPlayMode.Background)
            End If
        End If



        'Here is some code for the enemy movement

        framePen.DrawImage(My.Resources.Ayylien, AlienX, AlienY)
        If AlienY + My.Resources.Ayylien.Height > getHeight() + My.Resources.Ayylien.Height Then
            AlienX = i
            AlienY = 0 - 150
        End If

        framePen.DrawImage(My.Resources.UFO, UFOx, UFOy)
        If UFOy + My.Resources.UFO.Height > getHeight() + My.Resources.UFO.Height Then
            UFOx = i
            UFOy = 0 - 150
        End If
        'Here is some code for the ship's movement (the rest is below)

        If right = True Then
            shipX = shipX + 17
        End If
        If left = True Then
            shipX = shipX - 17
        End If


        'Here is some code for the enemy movement
        UFOy = UFOy + UFODx

        AlienY = AlienY + alienDx

        framePen.DrawImage(My.Resources.Ayylien, AlienX, AlienY)
        framePen.DrawImage(My.Resources.UFO, UFOx, UFOy)
        'Do not alter this line
        formPen.DrawImage(frame, 0, 0)
        'Check for collisions below


        If AlienBox.IntersectsWith(ShipBox) Then
            GameTimer.Stop()
            My.Computer.Audio.Play(My.Resources.Game_Over, AudioPlayMode.Background)
            MsgBox("GAME OVER")
            Me.Close()
        End If
        If UFOBox.IntersectsWith(ShipBox) Then
            GameTimer.Stop()
            My.Computer.Audio.Play(My.Resources.Game_Over, AudioPlayMode.Background)
            MsgBox("GAME OVER")
            Me.Close()
        End If
        If score = 4000 Then
            GameTimer.Stop()
            My.Computer.Audio.Play(My.Resources.You_Win, AudioPlayMode.Background)
            MsgBox("Congrats! On to the next level!")
        End If
        'When your score hits 2500, you will continue on to the next level (out of 3) 


    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Here is more of the code for the ship's movement
        If e.KeyValue = Keys.Space And projectileB = False Then
            projectileB = True
            projectileX = shipX + My.Resources.Ship.Width / 2 - 7
            projectileY = shipY
        End If
        'Pressing of the keys (ship and projectile)
        If e.KeyValue = Keys.D Then
            right = True
        End If
        If e.KeyValue = Keys.A Then
            left = True
        End If


    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        'Here are the release keys (ship & projectile)
        If e.KeyValue = Keys.D Then
            right = False
        End If
        If e.KeyValue = Keys.W Then
            up = False
        End If
        If e.KeyValue = Keys.S Then
            down = False
        End If
        If e.KeyValue = Keys.A Then
            left = False
        End If


    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Do not alter this code
        frame = New Bitmap(getWidth(), getHeight())
        formPen = Me.CreateGraphics
        framePen = Graphics.FromImage(frame)

        formPen.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        formPen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        formPen.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
        formPen.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality

        framePen.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        framePen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        framePen.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
        framePen.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality

        shipX = 215
        shipY = 450
        AlienX = 150


        GameTimer.Start()

    End Sub

    Function getWidth() As Integer
        'Returns the usable width of the form
        Return Me.ClientSize.Width
    End Function
    Function getHeight() As Integer
        'Returns the usable height of the form
        Return Me.ClientSize.Height
    End Function
End Class
