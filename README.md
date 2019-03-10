# Hackerthon
HGU 1st Plaython

일자: 2019/3/10
장소: 한동대학교
인원: 4명

- 기획단계 
  
  스토리: 공기오염으로 인해 생명력이 얼마 남지 않은 당신, 이미 좀비로 변해버린 자들을 피해 그곳에서 탈출하라!
  
  전체 게임 설명: 시간이 지남에 따라 계속해서 체력(life)이 줄어들며, 건물 곳곳에 비치된 구급상자(item)를 먹으면 일정 부분 회복된다. 건물 곳곳에 좀비가 돌아다니며, 플레이어를 감지하면 다가와 공격한다. 플레이어는 좀비에게 감지될 시 곳곳에 비치된 락커에 들어가 숨거나 무기를 사용해 좀비를 죽일 수 있다. 플레이어는 생명력이 다 떨어지기 전에 모든 열쇠를 모아서 그 곳을 탈출해야 한다.
  
  지하: 튜토리얼 및 게임 설명 / 소수의 좀비가 돌아다닌다. 열쇠 4조각을 찾아 지상 1층으로 올라가야 한다.
  
  지상 1,2층: 다수의 좀비가 돌아다닌다. 곳곳에 흩어진 열쇠조각 4개를 찾아서 건물에서 탈출해서 오두막으로 도망쳐야 한다. 키를 모두 찾으면 지상 1층의 문을 통해 외부로 나갈 수 있다. 열쇠는 한동대를 연상시킬 만한 곳에 숨겨져 있다.
  
  오두막: 오두막은 엔딩을 위한 곳으로, 여기까지 왔으면 이제 헬리콥터가 플레이어를 구하러 온다.
        

- 개발 과정 
  
  1)	Player : 걷기, 무기사용(총), item 집기, 소유 item 보기, 락커에 숨기, 체력 소진 및 회복
  2)	Zombie : 리스폰, 걷기, 체력, player 감지 -> 공격, 사망, player 감지 해제
  3)	Item : 케비넷, 구급통 (치료약, 방독면) -> 정해진 위치, 내용 random, Key, 상호작용 (캐비넷)
  4)	Room : 조명, 배경음, 효과음, key, 소품
  5)	Gun : 발사, 효과, 장전, 총알

  이 중에서 실제로 개발 과정에서는 Gun 부분과, 이와 관련된 기능들도 제외했다.
  각자 부분을 구현 후, 오두막을 제외하고 지하 / 지상 1,2 층의 두 씬에서 각자의 부분을 포함시키면서 합쳐나갔다.


- 미구현 대상

  1)  플레이어가 무기(총, 도끼 등)를 사용하여 좀비를 공격할 수 있게 하는 것
  2)  지상 1,2층에서 열쇠를 모두 찾았을 시 건물 외부로 나갈 수 있는 것
  3)  오두막 부분
  
  
- 실수 및 구현이 어려웠던 내용

  cardboard를 이용한 VR로 구현하면서, 3D가 아닌 2D asset들을 다루는 부부에서 실수가 있었다. 유니티 상에서 게임을 재생했을 때는 2D 이미지가 잘 보였지만, 빌드 후 핸드폰 상에서 재생했을 때는 이미지가 보이지 않아 난관을 겪었다. 알고 보니, 캔버스 설정하는 부분에서 이미 주의사항으로 떠있던 부분이었는데, 제한된 시간안에 빠르게 만들다보니 미처 신경쓰지 못해 일어난 일이었다. 또한 컴퓨터에서 재생했을 때와, 핸드폰에서 기기를 쓰고 실제 보았을 때의 이미지 위치나 크기들이 조금 달랐는데, 시간이 없어 세세하게 신경쓰지 못한 점이 아쉬웠다. 
  
  player가 걷는 애니메이션을 구현하면서, 오픈 에셋(소스) 두개 (손전등, 손전등을 잡은 손)를 합쳐 사용했고, 애니메이션 2개를 사용하였는데, 걷는 것에 따라 움직이는 애니메이션(1)은 잘 구현이 되었는데, 숨쉬는 것에 따라 위아래로 움직이는 애니메이션(2)은 손전등을 잡은 손에만 구현이 되었다. 원래 오픈소스에서는 손전등 대신 총을 잡은 손이었는데, 총을 없애고 손전등 에셋을 추가하면서 생긴 문제인데, 해결하지 못한 채로 끝났다.
  
  좀비를 구현하면서 AI를 어떻게 구현하는지 감을 못잡고 있었을 때 다른 사람들의 구현을 참고하면서 Unity의 다양한 기능들과 기초적인 지식을 얻을 수 있었다. 그렇지만아무래도 타겟 플래폼이 핸드폰이다보니 사양적인 부분을 생각하지않고 실제 실행시 약간의 버벅거림이 있었다.
  
- 사용한 오픈소스 
1)	지상1층: https://assetstore.unity.com/packages/3d/environments/urban/basement-and-sewerage-modular-location-121248
2)	지하: https://assetstore.unity.com/packages/3d/environments/dungeons/decrepit-dungeon-lite-33936
3)	아이템(열쇠): https://assetstore.unity.com/packages/3d/handpainted-keys-42044
4)	아이템(구급상자): https://assetstore.unity.com/packages/3d/props/small-survival-pack-20565
5)  플레이어: https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351
            https://drive.google.com/open?id=1_3z4VP9otCbEgRjIapS01G_iflLKW3YZ (상업적 용도 사용 불가)
6)  손전등: https://assetstore.unity.com/packages/3d/props/electronics/flashlight-18972
7)  오디오: https://assetstore.unity.com/packages/audio/ambient/ambient-horror-sound-fx-free-64527
8)  좀비모델: https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232


- 참고한 자료,사이트 

[아이템 먹는 것]
https://unity3d.com/learn/tutorials/s/roll-ball-tutorial 
https://unity3d.com/learn/tutorials/s/survival-shooter-tutorial

[Player 애니메이션]
https://blog.naver.com/choish1919/221257856099

[좀비 애니메이션 적용]
https://www.youtube.com/watch?v=48hojHj3BQk
https://www.youtube.com/channel/UCqy5niYmcLgr4zBuc_ZAyIw

[좀비 AI 구현]
https://www.youtube.com/channel/UCqy5niYmcLgr4zBuc_ZAyIw

- 사진

<img width="1411" alt="해커톤" src="https://user-images.githubusercontent.com/40797315/54085815-5b1f4c80-4385-11e9-8c25-9029560f67cc.png">
<img width="1000" alt="스크린샷 2019-03-10 오후 11 03 04" src="https://user-images.githubusercontent.com/35189020/54086059-b3a41900-4388-11e9-9647-534578c4ce4b.png">
<img width="1000" alt="스크린샷 2019-03-10 오후 10 55 02" src="https://user-images.githubusercontent.com/35189020/54086016-26f95b00-4388-11e9-99b8-f2f6c2807952.png">
<img width="1435" alt="tutorial" src="https://user-images.githubusercontent.com/31753296/54083177-a163b400-4363-11e9-8693-ceb0536be4b6.png">
