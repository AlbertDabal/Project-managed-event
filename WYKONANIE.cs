using System;

public class Wykonanie { 
	public static void Wykonanie_event()
	{

        WYNIK_DIODA myNewForm = new WYNIK_DIODA();

        myNewForm.Visible = true;

        NOWY_EVENT.Visible = false;


        dioda_led.dioda = NazwaEvent.Text;



        myNewForm.nazwaevent.Text = dioda_led.dioda;



        if (SMD1.Checked)
        {
            myNewForm.dioda.Text = SMD1.Text;
        }
        else if (SMD2.Checked)
        {
            myNewForm.dioda.Text = SMD2.Text;
        }
        else
        {
            myNewForm.dioda.Text = SMD3.Text;
        }
    }
}
