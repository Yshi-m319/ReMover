// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Chladni"
{
    Properties
    {
        [NoScaleOffset]_Mask0   ("Mask0"    ,2D)="black"{}
        [NoScaleOffset]_Mask1   ("Mask1"    ,2D)="black"{}
        [NoScaleOffset]_Mask2   ("Mask2"    ,2D)="black"{}
        [NoScaleOffset]_Mask3   ("Mask3"    ,2D)="black"{}
        [NoScaleOffset]_Mask4   ("Mask4"    ,2D)="black"{}
        [NoScaleOffset]_Mask5   ("Mask5"    ,2D)="black"{}
        [NoScaleOffset]_Mask6   ("Mask6"    ,2D)="black"{}
        [NoScaleOffset]_Mask7   ("Mask7"    ,2D)="black"{}
        [NoScaleOffset]_HintMask0("HintMask0" ,2D)="black"{}
        [NoScaleOffset]_HintMask1("HintMask1" ,2D)="black"{}
        [NoScaleOffset]_HintMask2("HintMask2" ,2D)="black"{}
        [NoScaleOffset]_Light   ("Light"    ,2D)="black"{}//不変
        [NoScaleOffset]_Base    ("Base"     ,2D)="black"{}//不変
        [NoScaleOffset]_HintBase("HintBase" ,2D)="black"{}//不変
    }

    /*float4 frag(v2f_img i) : SV_Target
    {
        float d=distance(float2(0.5,0.5),i.uv);
        d*=30;
        d=abs(sin(d));
        d=step(0.5,d);
        return d;
    }*/
    
    SubShader 
    {
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include <UnityCG.cginc>

            //vertexシェーダーの引数
            struct appdata
            {
            float4 vertex:POSITION; //あらゆる座標を表す。Vertexシェーダーはまだレンダリングパイプラインで加工されていないのでPOSITIONがいいらしい
            float2 uv1:TEXCORD0;
            };

            struct v2f
            {
                float4 vertex:SV_POSITION;  //既にレンダリングパイプラインで加工されてるのでシステム上で扱われるSV_POSITIONが妥当らしい
                float2 uv1:TEXCOORD0;       //テクスチャUV。
            };
            sampler2D _Mask0;
            sampler2D _Mask1;
            sampler2D _Mask2;
            sampler2D _Mask3;
            sampler2D _Mask4;
            sampler2D _Mask5;
            sampler2D _Mask6;
            sampler2D _Mask7;
            sampler2D _HintMask0;
            sampler2D _HintMask1;
            sampler2D _HintMask2;
            sampler2D _Light;
            sampler2D _Base;
            sampler2D _HintBase;
            uniform float _AlphaT[8];
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex=UnityObjectToClipPos(v.vertex);//3D空間座標→スクリーン座標変換
                o.uv1=v.uv1;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target//SV_Target->ピクセルの色
            {
                
                const int hintSize=3;
                const int mskSize=8;
                fixed4 masks[mskSize];
                fixed4 hintMasks[hintSize];
                masks[0]=tex2D(_Mask0 ,1.0-i.uv1);
                masks[1]=tex2D(_Mask1 ,1.0-i.uv1);
                masks[2]=tex2D(_Mask2 ,1.0-i.uv1);
                masks[3]=tex2D(_Mask3 ,1.0-i.uv1);
                masks[4]=tex2D(_Mask4 ,1.0-i.uv1);
                masks[5]=tex2D(_Mask5 ,1.0-i.uv1);
                masks[6]=tex2D(_Mask6 ,1.0-i.uv1);
                masks[7]=tex2D(_Mask7 ,1.0-i.uv1);
                hintMasks[0]=tex2D(_HintMask0 ,1.0-i.uv1);
                hintMasks[1]=tex2D(_HintMask1 ,1.0-i.uv1);
                hintMasks[2]=tex2D(_HintMask2 ,1.0-i.uv1);
                fixed4 mask=masks[0];
                for (int j=1;j<mskSize;j++)
                {
                    mask+=masks[j]*_AlphaT[j];
                }
                
                fixed4 hintMask=hintMasks[0];
                for (int j=1;j<hintSize;j++)
                {
                    hintMask+=hintMasks[j];
                }
               // clip(mask);
                
                const fixed4 col=   tex2D(_Light,  i.uv1);
                const fixed4 base = tex2D(_Base,    i.uv1);
                fixed4 hint=        tex2D(_HintBase,i.uv1);
                hint=lerp(0,hint,hintMask);  //ヒント用テクスチャ
                return lerp(base,col,mask)+hint;     //音波が当たった時のテクスチャ
            }
            ENDCG
        }
        
        
    }
}
