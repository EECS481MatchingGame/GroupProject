Imports Microsoft.Kinect

'------------------------------------------------------------------------------
' <copyright file="KinectColorViewer.xaml.cs" company="Microsoft">
'     Copyright (c) Microsoft Corporation.  All rights reserved.
' </copyright>
'------------------------------------------------------------------------------

Namespace Microsoft.Samples.Kinect.WpfViewers

	''' <summary>
	''' Interaction logic for KinectColorViewer.xaml
	''' </summary>
	Partial Public Class KinectColorViewer
		Inherits ImageViewer
		Private Shared ReadOnly Bgr32BytesPerPixel As Integer = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8

		Private lastImageFormat As ColorImageFormat = ColorImageFormat.Undefined
		Private pixelData() As Byte
		Private outputImage As WriteableBitmap

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Sub OnKinectChanged(ByVal oldKinectSensor As KinectSensor, ByVal newKinectSensor As KinectSensor)
			If oldKinectSensor IsNot Nothing Then
				RemoveHandler oldKinectSensor.ColorFrameReady, AddressOf ColorImageReady
				kinectColorImage.Source = Nothing
				Me.lastImageFormat = ColorImageFormat.Undefined
			End If

			If newKinectSensor IsNot Nothing AndAlso newKinectSensor.Status = KinectStatus.Connected Then
				ResetFrameRateCounters()

				If newKinectSensor.ColorStream.Format = ColorImageFormat.RawYuvResolution640x480Fps15 Then
					Throw New NotImplementedException("RawYuv conversion is not yet implemented.")
				Else
					AddHandler newKinectSensor.ColorFrameReady, AddressOf ColorImageReady
				End If
			End If
		End Sub

		Private Sub ColorImageReady(ByVal sender As Object, ByVal e As ColorImageFrameReadyEventArgs)
			Using imageFrame As ColorImageFrame = e.OpenColorImageFrame()
				If imageFrame IsNot Nothing Then
					' We need to detect if the format has changed.
					Dim haveNewFormat As Boolean = Me.lastImageFormat <> imageFrame.Format

					If haveNewFormat Then
						Me.pixelData = New Byte(imageFrame.PixelDataLength - 1){}
					End If

					imageFrame.CopyPixelDataTo(Me.pixelData)

					' A WriteableBitmap is a WPF construct that enables resetting the Bits of the image.
					' This is more efficient than creating a new Bitmap every frame.
					If haveNewFormat Then
						kinectColorImage.Visibility = Visibility.Visible
						Me.outputImage = New WriteableBitmap(imageFrame.Width, imageFrame.Height, 96, 96, PixelFormats.Bgr32, Nothing) ' DpiY -  DpiX

						Me.kinectColorImage.Source = Me.outputImage
					End If

					Me.outputImage.WritePixels(New Int32Rect(0, 0, imageFrame.Width, imageFrame.Height), Me.pixelData, imageFrame.Width * Bgr32BytesPerPixel, 0)

					Me.lastImageFormat = imageFrame.Format

					UpdateFrameRate()
				End If
			End Using
		End Sub
	End Class
End Namespace
