# test-game
Produced by software engineering team
Product vision： It is a plot-based card-class game,you can experience real life as a player.At the beginning of the story,you are just an regular college student,and need to make the right choice in the face of difficulties ,temptations and various challenges.You may still encounter fierce monsters,including course (like higher mathematics),test(CET),and a wide variety of advanced programming languages(c++,java).Beat the monsters to gain their skills,and release the trick when needed(input “cout<<’hello word’;” to use C++-skill);You can constantly grow and learn in the game,and realize the way of the real society. eg:This product base is developed as a stand-alone game,network and database support is increased if conditions allow for later periods.



## 开发概要

不追求完全复刻《月圆之夜》，因为难度挺大的，敌人的配置，卡牌效果的配置都需要很多的考虑。如果追求不高，可以以基本完成项目用于展示为目标，所以我们有两个方向，任选其一

1. 着重设计一两个敌人，专注于展示一场比较完整的战斗。完整在于战斗系统及其效果的完整。

    - 实现UI动画效果

    - 体现出卡牌技能带来的动画效果

    - 给予敌人比较丰富的模型动画，让打斗看上去比较有趣又好看

    

2. 着重完成整个游戏的框架

    - 如开始界面、漫游界面、战斗界面之间的跳转
    - 上述界面要能承载其==基本的功能==（基本功能的描述待定）
    - 在战斗场景的视觉效果上不花太多功夫，使用少量甚至不使用美术资源。
    - 完成背包系统（用于展示玩家手头已拥有的卡牌的信息，类似于图鉴）
    - 完成角色系统（可以展示敌人的一些信息，类似于图鉴）



简要来说，前者注重于画面视觉效果，专注于设计一场战斗；而后者注重整个游戏系统，专注于展示游戏整体。后者相对来说更好实现，前者在画面上受限于审美和技术未必能呈现出预期效果。

在设计游戏的时候，希望可以简化整个游戏系统。可以参照其他一些不太复杂的回合制游戏，比如口袋妖怪（或如赛尔号、洛克王国等）



**本项目可能包含：**

1. 对战系统
2. 卡牌系统
3. 背包系统
4. 角色系统
5. Roguelike（想想就好）



### 对战系统

#### 参战

对战人数：两人（我方和敌方）

双方都需要有

> 血量值
>
> 魔力值（或许可以换一个更加切合本游戏主题的名称）
>
> 卡包（每回合需要在卡包中抽取一定量的卡牌）
>
> ……



#### 机制

回合制。

默认玩家先手，玩家完成操作之后，进入敌人的回合，不断轮转，直至一方战败（HP=0)，或者玩家主动退出战斗。

默认每次为双方各发送3张手牌。发牌前检测玩家手上是否有超过3张手牌，如果超过，强求玩家丢剩3张，然后才给玩家发牌。



### 卡牌系统（或说，技能系统）

普通攻击牌（造成物理伤害）

魔法攻击牌（消耗魔力值，造成魔法伤害）（==这里最好根据我们游戏主题，换个名字==）

补给牌（补血，补魔）

效果牌（给自己加buff，或者给敌人加debuff，或者在卡堆里额外抽牌）

#### buff

加强物理攻击

加强魔法攻击

加强防御

加强魔抗

#### debuff

削弱物抗

削弱魔抗

削弱物攻

削弱魔攻

烧伤、冻伤等（每回合扣血）（==这里最好根据我们游戏主题，换个名字==）

寄生种子（吸对方血）

睡眠（跳过回合）



### 背包系统

展示卡牌，在UI界面上，可以设计筛选算法。比如只显示魔法卡，只显示效果牌。也可以加上排序算法，比如魔法卡按cost值排序。



### 角色系统

在进入战斗之前，会在世界中游走（漫游界面），遇到各种角色。

1. 战斗者（敌人，可以和它战斗）

2. 医者（可以恢复血量）

3. 商人（可以购买卡牌）

4. ~~工匠（强化卡牌，这个可以不做）~~

    



## 开发思路

先把基本的卡牌系统和角色系统计好，为策划提供一个比较直观的配置工作流，从而快速设计大量卡牌和人物。策划在==前期设计卡牌特征和人物特征==，为程序开发提供方向，便于测试，后期使用再进行具体的数值上的配置。



程序分为三个模块开发：1.战斗模块；2.场景模块；3.背包模块



#### Github的使用

每个人都需要自己弄一个分支进行开发，而不是直接在main上做开发。等功能做完之后，再合并到main分支。

