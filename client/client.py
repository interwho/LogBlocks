import requests
import itertools
import random


class RequestGenerator(object):
    
    endpoints = {
        '/api/Users': 0.1,
        '/api/Authentication': 0.2,
        '/api/Posts': 0.3,
        '/api/Logout': 0.4,
    }

    def __init__(self, uri):
        self.uri = uri
        self.probabilities = [v for v in self.endpoints.values()]
        self.reverse_endpoint_mappings = {v:k for k,v in self.endpoints.items()}

    def generate_random_endpoint(self):
        totals = list(itertools.accumulate(self.probabilities))

        n = random.uniform(0, totals[-1])
        for i, total in enumerate(totals):
            if n <= total:
                return float(i)/10

    def make_random_requests(self, num_requests):
        i=0
        while i < num_requests:
            random_uri = self.reverse_endpoint_mappings[self.generate_random_endpoint()]
            resp = requests.get(self.uri + random_uri)
            print(resp)

if __name__ == '__main__':
    generator = RequestGenerator('http://45.79.134.56')
    generator.make_random_requests(10)
