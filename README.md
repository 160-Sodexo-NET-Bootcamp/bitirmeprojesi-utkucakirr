# Merhaba.

## Projeyi ayağa kaldırmak için, Database klasörü içerisinde tablo scripleri ve backup'ı verilen veritabanını kendi bilgisayarınızda kurmanız gerekmektedir.
## Ardından Hangfire için hangfire adında bir veritabanı daha kurmanız gerekmekte. Bu veritabanının içine herhangi bir tablo eklemeyiniz.

## Bu işlemlerin ardından WebAPI projesinde bulunan appsettings.json içerisindeki "DefaultDbConnection" ve "DefaultHangfireConnection" kısımlarını kendi connection stringlerinize göre doldurmanız gerekmektedir.

## Bütün bu işlemlerin ardından proje ayağa kalkmak için hazır durumdadır.

### Projeyi ayağa kaldırdıktan sonra email ve şifreniz ile öncelikle Register olmanız, başarılı olma durumunda ise ardından Login olmanız gerekmektedir.
### Başarılı bir şekilde login olduktan sonra size dönülen Jwt tokenı "Authorization" headerına "Bearer 'token'" olacak şekilde eklemeniz gerekmektedir.
### Register ve Login işlemleri dışında ki tüm işlemler yetki gerektirdiğinden dolayı bu işlemi gerçekleştirmezseniz uygulamayı kullanamazsınız.

### Yukarıda bulunan her şeyi yaptıktan sonra uygulamayı kullanmaya hazırsınız.
