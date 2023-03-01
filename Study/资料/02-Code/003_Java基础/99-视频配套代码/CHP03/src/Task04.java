import java.util.Scanner;

public class Task04 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

		//Êó£¬Å££¬»¢£¬ÍÃ£¬Áú£¬Éß£¬Âí£¬Ñò£¬ºï£¬¼¦£¬¹·£¬Öí
		//4	  5     6    7     8     9    10   11  0      1    2     3
		Scanner input = new Scanner(System.in);
		System.out.println("ÇëÊäÈëÄê·İ:");
		int year = input.nextInt();
		switch (year%12) {
		case 0: System.out.println("ºï");  break;
		case 1: System.out.println("¼¦");   break;
		case 2: System.out.println("¹·");   break;
		case 3: System.out.println("Öí");  break;
		case 4: System.out.println("Êó");  break;
		case 5: System.out.println("Å£");  break;
		case 6: System.out.println("»¢");   break;
		case 7: System.out.println("ÍÃ");  break;
		case 8: System.out.println("Áú");  break;
		case 9: System.out.println("Éß");  break;
		case 10: System.out.println("Âí");  break;
		case 11: System.out.println("Ñò");  break;
		default:
			break;
		}
		
	}

}
