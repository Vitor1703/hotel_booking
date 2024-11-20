# **HotelBooking** 🏨

Sistema para gerenciar reservas de hotel, com funcionalidades de criação, edição e exclusão de **Guests**, **Rooms** e **Bookings**. Este projeto foi desenvolvido em **.NET 8** e utiliza boas práticas de testes automatizados e cobertura de código.

---

## **📋 Funcionalidades**
- Gerenciamento de **reservas** (Booking).
- Gerenciamento de **hóspedes** (Guest).
- Gerenciamento de **quartos** (Room).
- API organizada em camadas de **Domínio**, **Aplicação**, e **Infraestrutura**.
- Testes automatizados com alta cobertura.

---

## **💾 Banco de Dados**
O projeto utiliza o **PostgreSQL** como banco de dados. Certifique-se de que ele esteja configurado e rodando antes de executar a aplicação.

### **Configuração do Banco de Dados**
1. Certifique-se de que o PostgreSQL está instalado.
2. Crie um banco de dados chamado `postgres`.
3. Atualize a string de conexão no arquivo de configuração da API:
