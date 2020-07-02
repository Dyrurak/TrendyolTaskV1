Feature: Trendyol
	

Background: 
* 'Chrome' driver ile browser acilir
Scenario: TrendyolGirisYaparakSepeteUrunEkleme
	* Trendyol sitesine gidilir	
	* Gelen popup ekrani kapatilir
	* Giris yap butonuna tiklanir
	* Eposta adresi 'test@gmail.com' olarak girilir
	* Sifre alanina 'testSifre' girilir
	* Giris yap butonuna basilarak giris yapilir
	* Bildirim popupı gelirse kapatılır
	* Kategorilere tiklanarak içindeki butikler kontrol edilir
	* Ilk butige tiklanarak içindeki ürünlerin görsellerinin yüklendiği kontrol edilir
	* Ilk urune tiklanir
	* Sepete ekle butonuna tiklanarak urun sepete eklenir
	