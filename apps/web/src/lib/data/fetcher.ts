/** Represents an HTTP request failure with the corresponding status code. */
export class HttpError extends Error {
  public readonly status: number;

  public constructor(message: string, status: number) {
    super(message);
    this.name = 'HttpError';
    this.status = status;
  }
}

const getErrorMessage = async (response: Response) => {
  try {
    const data = await response.json();
    if (typeof data?.message === 'string') {
      return data.message as string;
    }
  } catch {
    // Ignore JSON parsing errors and fallback to default message.
  }

  if (response.status === 401) {
    return 'Invalid credentials.';
  }

  return 'Request failed.';
};

/** Sends a JSON request and parses the JSON response, throwing HttpError on failure. */
export const jsonFetcher = async <TResponse>(
  input: RequestInfo | URL,
  init?: RequestInit,
): Promise<TResponse> => {
  const headers = new Headers(init?.headers);
  if (!headers.has('Content-Type')) {
    headers.set('Content-Type', 'application/json');
  }

  const response = await fetch(input, {
    ...init,
    headers,
  });

  if (!response.ok) {
    throw new HttpError(await getErrorMessage(response), response.status);
  }

  return response.json() as Promise<TResponse>;
};
