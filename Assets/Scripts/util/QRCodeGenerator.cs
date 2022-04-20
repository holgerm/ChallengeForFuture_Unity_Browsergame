using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRCodeGenerator : MonoBehaviour
{
    public RawImage qrImage;
    private BarcodeWriter qrWriter;
    private Texture2D qrTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        qrWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = 256,
                Height = 256
            }
        };

        qrTexture = new Texture2D(256, 256);
    }

    public void UpdateQRCode(string text)
    {
        Color32[] colors = qrWriter.Write(text);
        qrTexture.SetPixels32(colors);
        qrTexture.Apply();
        qrImage.texture = qrTexture;
    }
}
