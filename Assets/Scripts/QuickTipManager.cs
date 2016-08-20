using UnityEngine;
using System.Collections;

public class QuickTipManager : MonoBehaviour 
{
	public string[] tips;

	void OnLevelWasLoaded()
	{
		if(Language.currentLanguage == "English")
		{
			tips[0] = "Remember that you don't need to worry about mana points in Arcade mode.";
			tips[1] = "Each enemy is different when it comes to resistance to magic, attack power, speed etc. You can check their stats in Help section.";
			tips[2] = "In Arena mode it is crucial to make the best use of both physical and magical attacks. Don't waste mana on enemies which can be easily defeated with a sword";
			tips[3] = "Compare your score with people all around the globe by going to leaderboards.";
			tips[4] = "Check the Help section to learn the basics of the game.";
			tips[5] = "Do you like achievements? We do! You can always check how to get them by going to the main menu.";
			tips[6] = "Each spell has different effects and cause different statuses. You can learn about them in Help section.";
			tips[7] = "Don't be reckless! Control your health and use Recovery spell if needed";
			tips[8] = "Did you know that Devil Awaits spell changes all the spells on your hand? Use it wisely!";
			tips[9] = "Did you find any bugs? Do you have any advice how to improve the game? Please let me know!";
		}
		if(Language.currentLanguage == "Japanese")
		{
			tips[0] = "アルケード・モードでは、エネルギー・ポイントを気にしなくてもいい。";
			tips[1] = "モンスターの攻撃力、速度、魔法への耐久力などは、種類によって違う。それぞれの統計的データは、「ヘルプ」でチェックせよ。";
			tips[2] = "アリーナ・モードでは、魔法攻撃と物理攻撃を使いこなせるかどうかが、キーポイント！　剣で簡単に倒せるモンスターに魔法を使わないこと！";
			tips[3] = "リダーボーズで自分のスコアを世界中の人と比較してみよう。";
			tips[4] = "ゲーム操作を習うには、「ヘルプ」をチェックして。";
			tips[5] = "アチーブメントが好き? 僕もだよ!メニューでアチーブメント獲得の条件をチェックできるよ。";
			tips[6] = "魔法によって効果が違い、ステータス異常を起こすこともある。より詳しい情報は「ヘルプ」でチェックできるよ。";
			tips[7] = "無茶しないで! HＰに注意して、必要なときは、回復の魔法を使おう。";
			tips[8] = "「悪魔」魔法で選択できる魔法がスワップされるのは覚えている？　使うときは、利口な使い方をしてね!";
			tips[9] = "バグが見つかった？　ゲームを改良するアイディアがある？どちらも、僕に知らせてくださいね！";  
		}
		if(Language.currentLanguage == "Polish")
		{
			tips[0] = "Pamiętaj, że nie musisz martwić się o punkty energii w trybie Arcade.";
			tips[1] = "Każdy wróg rożni się pod względem siły ataku, szybkości, odporności na magię itd. Sprawdź statystyki każdego z nich w sekcji Help.";
			tips[2] = "W trybie Areny kluczowa jest strategia korzystania z ataków fizycznych i magicznych！Nie trać energii na wrogów, których możesz łatwo pokonać mieczem.";
			tips[3] = "Porównaj swój rezultat z ludźmi z całego świata w tabeli wyników.";
			tips[4] = "Zobacz sekcję Help, aby nauczyć się podstawowych zasad gry.";
			tips[5] = "Lubisz achievementy? Ja też! W menu głównym możesz sprawdzić jak możesz je zdobywać.";
			tips[6] = "Każdy czar ma inne efekty i wywołuje inne statusy. Możesz dowiedzieć się więcej w sekcji Help.";
			tips[7] = "Nie przesadzaj! Miej na uwadze swoje HP i korzystaj z czaru uzdrawiania,gdy zajdzie taka potrzeba.";
			tips[8] = "Czy wiesz, że czar [Dotyk Diabła] podmienia zaklęcia z puli? Korzystaj z tego mądrze!";
			tips[9] = "Natknąłeś się na błąd？Masz pomysł jak poprawić grę？Podziel się swoimi wrażeniami ze mną!";
		}
	}
}
