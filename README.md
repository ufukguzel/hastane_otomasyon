# Hastane Otomasyon Sistemi

Bu proje, bir hastane otomasyon sistemi geliştirmek amacıyla hazırlanmıştır. Sistem; hasta kayıtları, doktor yönetimi, randevu planlaması ve diğer hastane operasyonlarını kolaylaştırmayı hedefler.

## Özellikler

- **Hasta Yönetimi**: Hasta kayıtlarının oluşturulması, güncellenmesi ve silinmesi.
- **Doktor Yönetimi**: Doktor bilgilerini düzenleme ve listeleme.
- **Randevu Sistemi**: Hastalar için randevu oluşturma, güncelleme ve iptal etme.
- **Kullanıcı Girişi**: Yetkilendirme ve güvenli kullanıcı yönetimi.
- **Raporlama**: Sistem üzerinden raporların oluşturulması ve görüntülenmesi.

## Teknolojiler

Proje aşağıdaki teknolojilerle geliştirilmiştir:

- **Backend**: Python / Flask
- **Frontend**: HTML, CSS, JavaScript
- **Veritabanı**: MySQL
- **Diğer Araçlar**:
  - Bootstrap: Arayüz tasarımı için
  - SQLAlchemy: Veritabanı işlemleri için ORM

## Kurulum

1. **Gereksinimleri Yükleyin**:
   - Python 3.8+
   - MySQL

2. **Proje Dosyalarını İndirin**:
   ```bash
   git clone https://github.com/ufukguzel/hastane_otomasyon.git
   cd hastane_otomasyon
   ```

3. **Sanal Ortam Oluşturun**:
   ```bash
   python -m venv venv
   source venv/bin/activate  # Windows için: venv\Scripts\activate
   ```

4. **Bağımlılıkları Yükleyin**:
   ```bash
   pip install -r requirements.txt
   ```

5. **Veritabanını Ayarlayın**:
   - MySQL üzerinde bir veritabanı oluşturun:
     ```sql
     CREATE DATABASE hastane_otomasyon;
     ```
   - `config.py` dosyasındaki veritabanı bağlantı ayarlarını güncelleyin.

6. **Veritabanı Migrasyonlarını Çalıştırın**:
   ```bash
   flask db upgrade
   ```

7. **Uygulamayı Başlatın**:
   ```bash
   flask run
   ```

   Uygulama, [http://localhost:5000](http://localhost:5000) adresinde çalışacaktır.

## Kullanım

1. Sisteme giriş yaparak kullanıcı yetkilerinize göre ilgili modülleri kullanabilirsiniz.
2. Hasta, doktor ve randevu işlemlerini gerçekleştirebilirsiniz.
3. Yönetici olarak raporlama ekranlarına erişebilirsiniz.

## Katkıda Bulunma

Katkıda bulunmak isterseniz aşağıdaki adımları takip edin:

1. Bu projeyi forklayın.
2. Yeni bir özellik veya düzeltme için bir dal oluşturun.
   ```bash
   git checkout -b yeni-ozellik
   ```
3. Değişikliklerinizi yapın ve commit'leyin.
   ```bash
   git commit -m "Yeni bir özellik ekledim"
   ```
4. Değişiklikleri dalınıza push'layın.
   ```bash
   git push origin yeni-ozellik
   ```
5. Bir Pull Request (PR) oluşturun.

## Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.

---

### İletişim
Herhangi bir soru ya da öneri için benimle iletişime geçebilirsiniz:
- **E-posta**: ufukguzel@example.com
- **GitHub**: [@ufukguzel](https://github.com/ufukguzel)
