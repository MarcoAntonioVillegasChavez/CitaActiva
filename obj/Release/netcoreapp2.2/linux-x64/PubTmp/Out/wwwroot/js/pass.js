/* Password strength indicator */
function passwordStrength(password) {

	var msg = ['No aceptable', 'No recomendable', 'Normal', 'Aceptable', 'Buena', 'Excelente'];

	var desc = ['0%', '20%', '40%', '60%', '80%', '100%'];
	
    var descClass = ['', 'bg-danger', 'bg-warning', 'bg-warning', 'bg-success', 'bg-success'];

	var score = 0;

    if (password.length == 0) score = 0;

	// if password bigger than 6 give 1 point
	if (password.length > 6) score++;

	// if password has both lower and uppercase characters give 1 point	
	if ((password.match(/[a-z]/)) && (password.match(/[A-Z]/))) score++;

	// if password has at least one number give 1 point
	if (password.match(/\d+/)) score++;

	// if password has at least one special caracther give 1 point
	if ( password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/) )	score++;

	// if password bigger than 12 give another 1 point
	if (password.length > 10) score++;
	
	// Display indicator graphic
	$(".jak_pstrength").removeClass(descClass[score-1]).addClass(descClass[score]).css( "width", desc[score] );

	// Display indicator text
	$("#jak_pstrength_text").html(msg[score]);

	// Output the score to the console log, can be removed when used live.
    console.log(desc[score]);
}