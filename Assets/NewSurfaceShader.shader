Shader "Unlit/Scan_Code02"
{
    Properties
    {
        [Header(RenderingMode)]
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("SrcBlend" ,int   )=0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("DstBlend",int)=0
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull",int)=0

        [Header(Base)]
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color",color)=(1,1,1,0.2)
        _Instensity("Instensity",float)=0.5
        _MainUVSpeedX("MainUVSpeed X",float)=1
        _MainUVSpeedY("MainUVSpeed Y",float)=1

        [Header(Mask)]
        [Toggle]_MASKENABLED("Mask Enable",int)=0
         _MaskTex("MaskTex",2D)="white"{}
        _MaskUVSpeedX("MaskUVSpeed X",float)=0
        _MaskUVSpeedY("MaskUVSpeed Y",float)=0

        [Header(Distort)]
        [MaterialToggle(DISTORTENABLED)]_DISTORTENABLED_ON("Distort Enable",int)=0
        _DistortTex("DistortTex",2d)="white"{}
        _Distort("Distort",Range(0,1))=0
        _DistortUVSpeedX("DistortUVSpeed X",float)=0
        _DistortUVSpeedY("DistortUVSpeed Y",float)=0
    }
    SubShader
    {
        Blend [_SrcBlend][_DstBlend]
        Tags { "Queue"="Transparent" }       
        Cull [_Cull]
        Pass
        {            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag           
            #pragma shader_feature _ _MASKENABLED_ON
            #pragma shader_feature _ DISTORTENABLED
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;            
                float4 vertex : SV_POSITION;
                float2 uv2:TEXCOORD1;
            };  

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Instensity;
            float _MainUVSpeedX,_MainUVSpeedY;
            sampler2D _MaskTex;
            float4 _MaskTex_ST;//Mask贴图的UV元素
            float _MaskUVSpeedX,_MaskUVSpeedY;//控制MaskUV的变量
            sampler2D _DistortTex;
            float4 _DistortTex_ST;
            float _Distort;
            float _DistortUVSpeedX,_DistortUVSpeedY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy=v.uv*_MainTex_ST.xy+_MainTex_ST.zw+float2(_MainUVSpeedX,_MainUVSpeedY)*_Time.y;//主贴图UV流动
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex)+float2(_MainUVSpeedX,_MainUVSpeedY)*_Time.y;            
                #if _MASKENABLED_ON
                    o.uv.zw=TRANSFORM_TEX(v.uv,_MaskTex)+float2(_MaskUVSpeedX,_MaskUVSpeedY)*_Time.y;//Mask贴图UV流动
                #endif

                #if DISTORTENABLED
                o.uv2=TRANSFORM_TEX(v.uv,_DistortTex)+float2(_DistortUVSpeedX,_DistortUVSpeedY)*_Time.y;
                #endif
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 distort=i.uv.xy;
                
                #if DISTORTENABLED
                    fixed4 distortTex=tex2D(_DistortTex,i.uv2);
                    distort=lerp(i.uv.xy,distortTex,_Distort);
                #endif

                fixed4 c = tex2D(_MainTex, distort);  
                c*=_Color*_Instensity;     

                #if  _MASKENABLED_ON
                    fixed4 maskTex=tex2D(_MaskTex,i.uv.zw); 
                    c*=maskTex;               
                #endif  
                return c;
            }
            ENDCG
        }
    }
}
