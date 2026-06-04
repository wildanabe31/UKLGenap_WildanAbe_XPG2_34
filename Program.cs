List<Stand> daftar = new List<Stand>()
{
    new StandOutdoor("Outdoor 1", 400000),
    new StandOutdoor("Outdoor 2", 500000),
    new StandIndoor("Indoor 1", 700000),
    new StandIndoor("Indoor 2", 800000),
    new StandPremium("Premium 1", 1800000),
    new StandPremium("Premium 2", 2000000)
};

while (true)
{
    Console.WriteLine("\n===================");
    Console.WriteLine("\n-Sewa Stand Malkot-\n");
    Console.WriteLine("Daftar stand: ");
    foreach (var stand in daftar)
    {
        stand.Info();
    }
    Console.WriteLine("\nMenu: \n1. Sewa Stand\n2. Akhiri Sewa \n3. Keluar");
    Console.Write("Pilih menu: ");

    if (!int.TryParse(Console.ReadLine(), out int pilihan))
    {
        Console.WriteLine("\nPilihan harus berupa angka!");
        continue;
    }

    switch (pilihan)
    {
        case 1:
            Console.Write("\nMasukkan nama stand: ");
            string nama = Console.ReadLine();
            var cari = daftar.FirstOrDefault(ck => string.Equals(nama, ck.Nama, StringComparison.OrdinalIgnoreCase));
            if (cari == null)
            {
                Console.WriteLine("\nStand tidak ditemukan!");
            }
            else if (!cari.Available)
            {
                Console.WriteLine("\nStand sudah disewa!");
            }
            else
            {
                Console.WriteLine($"Stand {cari.Nama} tersedia");
                Console.Write("\nLama sewa: ");

                if (int.TryParse(Console.ReadLine(), out int hari))
                {
                    if (hari <= 0)
                    {
                        Console.WriteLine("Lama sewa harus lebih dari 0!");
                    }
                    else
                    {
                        double total = cari.Total(hari);
                        Console.WriteLine($"Total biaya: {total}");
                        cari.UbahStatus();
                        Console.WriteLine("\nSewa berhasil!");
                    }
                }
                else
                {
                    Console.WriteLine("Lama sewa harus berupa angka!");
                }
            }
            break;

        case 2:
            Console.WriteLine($"\nDaftar stand yang disewakan: ");
            foreach (var stand in daftar.Where(cariStand => !cariStand.Available))
            {
                stand.Info();
            }
            Console.Write("\nMasukkan nama stand: ");
            string nama2 = Console.ReadLine();
            var cari2 = daftar.FirstOrDefault(ck => string.Equals(nama2, ck.Nama, StringComparison.OrdinalIgnoreCase));
            if (cari2 == null)
            {
                Console.WriteLine("\nStand tidak ditemukan!");
            }
            else if (cari2.Available)
            {
                Console.WriteLine("\nStand belum disewa!");
            }
            else
            {
                cari2.UbahStatus();
                Console.WriteLine("\nSewa berhasil diakhiri!");
            }
            break;

        case 3:
            Console.WriteLine("\nTerimakasih!");
            Console.WriteLine("\nTekan enter untuk keluar");
            Console.ReadLine();
            return;

        default:
            Console.WriteLine("\nPilihan tidak valid!");
            break;
    }
}
class Stand
{
    protected string _nama;
    protected double _harga;
    protected bool _available;

    public Stand(string nama, double harga)
    {
        _nama = nama;
        _harga = harga;
        _available = true;
    }

    public string Nama
    {
        get { return _nama; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Nama tidak boleh kosong!");
            }
            else
            {
                _nama = value;
            }
        }
    }

    public double Harga
    {
        get { return _harga; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Harga tidak boleh negatif!");
            }
            else
            {
                _harga = value;
            }
        }
    }

    public bool Available
    {
        get { return _available; }
    }

    public void Info()
    {
        string status = _available ? "Tersedia" : "Tidak Tersedia";
        Console.WriteLine($"{_nama,-12} | {Harga,-10} | {status}");
    }

    public void UbahStatus()
    {
        _available = !_available;
    }

    public virtual double Total(int hari)
    {
        return _harga * hari;
    }
}

class StandOutdoor : Stand
{
    protected double _tenda;
    public StandOutdoor(string nama, double harga) : base(nama, harga)
    {
        _tenda = 75000;
    }
    public override double Total(int hari)
    {
        return base.Total(hari) + (_tenda * hari);
    }
}

class StandIndoor : Stand
{
    protected double _listrik;
    public StandIndoor(string nama, double harga) : base(nama, harga)
    {
        _listrik = 100000;
    }
    public override double Total(int hari)
    {
        return base.Total(hari) + (_listrik * hari);
    }
}

class StandPremium : Stand
{
    protected double _keamanan;
    public StandPremium(string nama, double harga) : base(nama, harga)
    {
        _keamanan = 300000;
    }
    public override double Total(int hari)
    {
        return base.Total(hari) + _keamanan;
    }
}