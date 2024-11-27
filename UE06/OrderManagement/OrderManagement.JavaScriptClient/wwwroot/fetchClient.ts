const ORDER_MANAGEMENT_BASE_URI = "https://localhost:7022"; // aus OrderManagement.API/Properties/launchSettings.json (https / applicationUrl)

interface Customer {
  id:      string;
  name:    string;
  zipCode: number;
  city:    string;
  rating:  string;
}


function fetchCustomerById(id: string): Promise<Customer> {
  if (!id)
    throw new Error('ID must not be empty');

  return fetch(
    `${ORDER_MANAGEMENT_BASE_URI}/api/customers/${id}`, {
       method: "GET",
       headers: { 'Accept': 'application/json' }
    })
    .then(response => {
      if (!response.ok)
        throw new Error(`Failed with status code ${response.status}`)

      return response.json().then(data => {
        return Promise.resolve(data);
      });
    });
}

async function fetchCustomerByIdAsync(id: string): Promise<Customer> {
  if (!id)
    throw new Error('ID must not be empty');

  const response = await fetch(`${ORDER_MANAGEMENT_BASE_URI}/api/customers/${id}`, {
    method: "GET",
    headers: { 'Accept': 'application/json' },
  });

  if (response.status !== 200)
    throw new Error(`Failed with status code ${response.status}`);
  return await response.json();
}
