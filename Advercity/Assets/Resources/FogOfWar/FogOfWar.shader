Shader "Custom/FogOfWar" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Player_Pos_0 ("player_0",Vector)=(5,0,0,1)
		_Player_Pos_1 ("player_1",Vector)=(0,0,0,1)
		_Player_Pos_2 ("player_2",Vector)=(0,0,0,1)
		_Player_Pos_3 ("player_3",Vector)=(0,0,0,1)
		_Player_Pos_4 ("player_4",Vector)=(0,0,0,1)
		_Player_Pos_5 ("player_5",Vector)=(0,0,0,1)
		_Player_Pos_6 ("player_6",Vector)=(0,0,0,1)
		_Player_Pos_7 ("player_7",Vector)=(0,0,0,1)
		_player_per0("per_0",float)=0
		_player_per1("per_1",float)=0
		_player_per2("per_2",float)=0
		_player_per3("per_3",float)=0
		_player_per4("per_4",float)=0
		_player_per5("per_5",float)=0
		_player_per6("per_6",float)=0
		_player_per7("per_7",float)=0
		
		
	}
	SubShader {
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert alpha:auto

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 location;
		};
		
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _Player_Pos_0;
		float _player_per0;
		//float4 _Player_Pos[8];
		//float _Player_per[8];
		void vert(inout appdata_full vertexData, out Input outData) {
			float4 posWorld= mul(_Object2World, vertexData.vertex);
			outData.uv_MainTex=vertexData.texcoord;
			outData.location=posWorld.xz;
			
		}
		float powerForPos(float4 pos, float2 nearVertex, int player){
			//float atten= clamp(_Player_per[player]-length(pos.xz-nearVertex.xy),0.0,_Player_per[player]);
			//return (1.0/100)*atten/_Player_per[player];
			float atten= clamp(_player_per0-length(pos.xz-nearVertex.xy),0.0,_player_per0);
			return (1.0/0.5)*atten/_player_per0;
		}
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float alpha=(1.0-(c.a+powerForPos(_Player_Pos_0,IN.location, 0)));
			for(int i=0; i<8;i++){
				//alpha+=powerForPos(_Player_Pos[i],IN.location, i);
			}
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = alpha;
		}
		ENDCG
	} 
	FallBack "Transparent"
}
