const urlUser = 'https://localhost:7107/api/User';
const urlWallet = 'https://localhost:7107/api/Wallet';

export const fetchUsers = async () => {
    const response = await fetch(`${urlUser}`);
    if (!response.ok) {
        throw new Error('Failed to fetch users');
    }
    return response.json();
};

export const fetchUserById = async (id) => {
    const response = await fetch(`${urlUser}/${id}`);
    if (!response.ok) {
        throw new Error('Failed to fetch user');
    }
    return response.json();
};

export const createUser = async (user) => {
    const response = await fetch(`${urlUser}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
    });
    if (!response.ok) {
        throw new Error('Failed to create user');
    }
    return response.json();
};

export const updateUser = async (user) => {
    const response = await fetch(`${urlUser}/${user.id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
    });
    if (!response.ok) {
        throw new Error('Failed to update user');
    }
    return response.json();
};

export const deleteUser = async (id) => {
    const response = await fetch(`${urlUser}/${id}`, {
        method: 'DELETE',
    });
    if (!response.ok) {
        throw new Error('Failed to delete user');
    }
    return response.json();
};

export const getUserWallets = async (userId) => {
    const response = await fetch(`${urlWallet}/${userId}`);
    return response.json();
};

export const deleteWallet = async (userId, currencyCode) => {
    await fetch(`${urlWallet}/${userId}/${currencyCode}`, {
        method: 'DELETE'
    });
};

export const convertCurrency = async (request) => {
    await fetch(`${urlWallet}/convert`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    });
};

export const addOrUpdateWallet = async (wallet) => {
    await fetch(`${urlWallet}/addOrUpdate`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(wallet)
    });
};;

export const fetchCurrencyRates = async () => {
    const response = await fetch('https://api.nbp.pl/api/exchangerates/tables/A?format=json');
    if (!response.ok) {
        throw new Error('Failed to fetch currency rates');
    }

    const data = await response.json();
    // Dodanie waluty PLN z kursem 1
    const pln = {
        currency: 'polski złoty',
        code: 'PLN',
        mid: 1,
    };
    data[0].rates.push(pln);
    return data;
};

export const fetchCurrencyRatesDays = async () => {
    const responseDays = await fetch('https://api.nbp.pl/api/exchangerates/tables/a/last/5?format=json');
    const dataDays = await responseDays.json();
    return dataDays;
};

export const fetchCurrencyHistory = async (code) => {
    const responseHistory = await fetch(`https://api.nbp.pl/api/exchangerates/rates/a/${code}/last/5?format=json`);
    const dataHistory = await responseHistory.json();
    return dataHistory.rates;
};
