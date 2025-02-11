# SELLCT

## 目次
- [作品概要](#作品概要)
- [使用している技術・考え方](#使用している技術考え方)
- [担当箇所](#担当箇所)
- [主な担当スクリプト解説](#主な担当スクリプト解説)
- [実装が難しかった点と対策](#実装が難しかった点と対策)
- [改善点](#改善点)
- [制作期間](#制作期間)
  
## 作品概要
### 制作目的
日本ゲーム大賞2023「アマチュア部門」で大賞を狙うことを目的にチームで制作しました。

### 日本ゲーム大賞 テーマ「こだわり」
提出時にこだわったポイントをチームメンバーの企画者がまとめました。

> この作品のこだわったポイントは「新奇性」による驚きです。昨今のゲームは新しさに欠けていると言えます。既存のゲームと類似のゲームシステムを持ったゲームが非常に多くを占めているのが現状です。同じような内容のゲームであれば既にあるゲームを遊べば良いでしょう。だからこそ、私達はゲームには驚きを持った「新奇性」があるべきだと考え、その一点にこだわって作りました。このゲームのユニークな所は、ゲーム内でゲームの構成要素が商品として取引されることにあります。取引されることでゲームの構成要素を獲得したり、失ったりすることになります。今までのゲームでは、変化することのなかった、ゲームを構成する要素が商品として取引され、変化する。これが私達のゲームにおける新奇性になります。また新奇性の体験としての価値は驚きにあると考えています。今までの常識から外れたことが起きる想定外の驚きが新奇性の価値です。商品の取引で今までのゲームになかったことが起きる瞬間には、より驚きが生まれるように演出にもこだわっています。またこの作品最後に待っている最大の驚きは他のどのゲームでも得ることが出来ない体験になるでしょう。ぜひお楽しみください。

### 紹介コメント
> 落ちてきた。覚えていたのは、ただそれだけだった。霧の立ち込める暗い洞窟。「奈落」と呼ばれるそこから脱出を目指して、君は不思議な商人達と取引をする。異常なまでの輝きへの執着を持つ「没落貴族」や耳で感じ取れる世界が全ての「耳長の獣人」など 個性的な商人達と売買を繰り返し、君は無事に元の世界へ帰れるだろうか

## 使用している技術・考え方
- SOLID原則
- DRY原則
- デザインパターン
  - シングルトンパターン
  - Mediatorパターン
  - Observerパターン
- 値オブジェクト
- 拡張メソッド

## 担当箇所
- `Scriptsフォルダ`にある`DataManagementフォルダ` `Endingフォルダ`を**除いた**プログラム
- 紹介動画制作

## 主な担当スクリプト解説
担当スクリプトは、上記担当箇所の通り多岐に渡ります。中でも自信のあるスクリプト、または技術を使用しているスクリプトを一部ピックアップして紹介します。

- [Card.cs](SELLCT/Assets/Scripts/Ingame/Element/Card.cs)
  - カードを売買した際に、それぞれのカードがどのような効果をもたらすかを実装する際の抽象クラスです。
  - インスペクターから管理したいため、interfaceではなくclassとして実装しています。
  - オブジェクト指向の三要素(継承・カプセル化・ポリモーフィズム)をすべて使用しているソースコードになります。
- [TimeLimit.cs](SELLCT/Assets/Scripts/Ingame/TradingPhase/ValueObject/TimeLimit.cs)
  - 制限時間の値オブジェクトです。
  - `Add()`などの引数に当クラスを使用することで、想定外の値の代入や人為的ミスの軽減が目的です。
    - 制限時間の減少は頻繁に行われる処理です。よって不変でインスタンスを生成するのではなく、可変にして、コストを削減しています。
- [Money.cs](SELLCT/Assets/Scripts/Ingame/TradingPhase/ValueObject/Money.cs)
  - お金の値オブジェクトです。
  - `TimeLimit.cs`とは異なり、不変を活用しています。
    - お金は頻繁に増減しない処理だからです。
  - Moneyクラス同士で比較が出来るように演算子オーバーロードを用いています。
- [EEX_null.cs](SELLCT/Assets/Scripts/Ingame/Element/EEX_null.cs)
  - Cardクラスを継承したそれぞれのカードを表すクラスのうちの1つです。
  - 命名規則を E〇_◆◆(◯・・・ID、◆◆・・・名前)としています。
    - 基本的なC#の命名規則に違反していますが、わかりやすさ、管理しやすさのためあえてこのようにしています。
  - 特徴として、nullチェックのかわりにこのクラスかどうかを判定させるようにしています。
    - これは反省点としてのピックアップです。私は制作者のため理解していましたが、周知させたところでただの混乱の元になっていたように感じます。
- [PhaseController.cs](SELLCT/Assets/Scripts/Ingame/PhaseController.cs)
  - フェーズの進行に責任を持つクラスです。
  - Observerパターンを用いています。

## 実装が難しかった点と対策
### 難しかった点
[良いコード／悪いコードで学ぶ設計入門](https://gihyo.jp/book/2022/978-4-297-12783-1)で得た知識を初めて実際にUnityで扱う部分です。
### 対策
基本は、覚えるために技術本に書いてあるサンプルコードの通りに作成するようにしました。

しかし、Unityで扱う上で一部工夫をしました。

例えば、不変の活用をする値オブジェクトでは、毎フレーム関わる処理を毎回インスタンスを生成するのではなく、可変にするといった工夫をしています。

## 改善点
ゲームデザイナーから「レベルデザインがしにくい」という指摘を受けました。私も実感していた部分ですので、対策として以下を制作しました。

- CSV でレベルデザイン出来るように CSV を自動生成するプログラムの作成
- 使い方のマニュアルの作成

上記のマニュアル・プログラムは現在進行中の[新規プロジェクト](https://github.com/GTM106/BalloonGame)のREADMEから確認が出来ます。

## 制作期間
2023/02/26 ~ 2023/05/31 (3ヶ月 5日)
