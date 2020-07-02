Feature: ApiTest
	

Background:
	* Apinin boş olup olmadığı kontrol edilir
Scenario: ApiTestVerifyAuthorIsRequired
	* Apide author girilmeden kitap eklenmeye çalışılır
Scenario: ApiTestVerifyTitleIsRequired
	* Apide title girilmeden kitap eklenmeye çalışılır
Scenario: ApiTestVerifyAuthorCanNotBeEmpty
	* Apide author boş girilerek kitap eklenmeye çalışılır
Scenario: ApiTestVerifyTitleCanNotBeEmpty
	* Apide title boş girilerek kitap eklenmeye çalışılır
Scenario: ApiTestVerifyIdIsReadOnly
	* Apide Id girilerek kitap eklenmeye çalışılır
Scenario: ApiTestVerifyBookCanBeCreated
	* Apide 'author' yazarının 'title' kitabı eklenir
	* Apiye oluşturulan kitap idsi ile GetRequest atıldığında oluşturulan kitabın döndüğü görülür
Scenario: ApiTestVerifyThatBooksWithSameAuthorAndTitleCanNotBeCreated
	* Apide 'author' yazarının 'title' kitabı eklenir
	* Apide 'author' yazarının 'title' kitabının tekrar eklenemediği görülür
	
